using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    [SerializeField]
    private bool _isPlayerOne;

    public bool IsPlayerOne => _isPlayerOne;

    [SerializeField]
    private PlayerControllerSettings _settingsSO;
    private Rigidbody2D rb;
    private Collider2D collider2d;
    public LayerMask groundLayer;
    [SerializeField]


    private Vector2 move;

    private enum JumpState {CantJump, CanJump, Jumping}
    private enum DashState {CantDash, CanDash, Dashing, ReloadDash}

    [SerializeField]
    private JumpState _jumpState = JumpState.CanJump;
    private bool _isGrounded;

    [SerializeField]
    private DashState _dashState = DashState.CanDash;

    public bool canMove = true;

    private float _dashTimer;

    private IEnumerator Dashed(bool cooldown = false)
    {
        Debug.Log("Start dash");
        yield return new WaitForSeconds(_settingsSO.DashDuration);
        Debug.Log("Dash end");
        rb.linearVelocityX = 0f;
        if(_dashState == DashState.Dashing)
            _dashState = DashState.CantDash;
        if(cooldown && _jumpState != JumpState.Jumping)
            StartCoroutine(FloorDashCooldown());
    }

    private IEnumerator FloorDashCooldown()
    {
        _dashState = DashState.ReloadDash;
        Debug.Log("Start cooldown");
        yield return new WaitForSeconds(_settingsSO.FloorDashCooldown);
        Debug.Log("EndCooldown");
        if(_jumpState != JumpState.Jumping)
            _dashState = DashState.CantDash;
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.SwitchCurrentActionMap(_isPlayerOne ? "Player1" : "Player2");
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(canMove) {
            if (_dashState == DashState.CantDash && _isGrounded)
                _dashState = DashState.CanDash;

            rb.gravityScale = _dashState == DashState.Dashing ? _settingsSO.DashAirGravityScale : _settingsSO.MovingGravityScale;
            transform.Translate(move * _settingsSO.Speed * (_jumpState == JumpState.Jumping ? _settingsSO.AirSpeedCoef : 1f) * Time.deltaTime);
            JumpBehaviour();
        }
        else {
            _dashState = DashState.CantDash;
            _jumpState = JumpState.CantJump;
            _isGrounded = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(!canMove) return;
        Vector2 inputMove = context.action.ReadValue<Vector2>().normalized;
        move = new Vector2(inputMove.x, move.y);
        if(move.x != 0f && _dashState != DashState.Dashing)
            transform.localScale = new Vector2(move.x < 0f ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    private void JumpBehaviour()
    {
        if(!canMove) return;
        _isGrounded = IsGrounded();
        switch (_jumpState)
        {
            case JumpState.Jumping:
                if (_isGrounded && rb.linearVelocityY < 0f)
                    _jumpState = JumpState.CanJump;
                    rb.linearVelocityY -= _settingsSO.AirGravityForce * Time.deltaTime * (_dashState == DashState.Dashing ? _settingsSO.DashAirGravityScale : 1f);
                break;

            default:
                _jumpState = _isGrounded ? JumpState.CanJump : JumpState.CantJump;
                break;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(!canMove) return;
        if (_isGrounded && _jumpState == JumpState.CanJump)
        {
            _jumpState = JumpState.Jumping;
            StopCoroutine(Dashed());
            _dashState = DashState.CanDash;
            rb.linearVelocityY += _settingsSO.JumpForce;
            Debug.Log($"{name} jumped");
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(!canMove) return;
        if (_dashState == DashState.CanDash)
        {
            _dashState = DashState.Dashing;
            StartCoroutine(Dashed(IsGrounded()));
            rb.linearVelocityX += _settingsSO.DashForce * Mathf.Sign(transform.localScale.x);
            rb.linearVelocityY *= _settingsSO.DashAirGravityScale;

            //Debug.Log($"{name} dashed");
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.IsTouchingLayers(collider2d, groundLayer);
    }
    

}
