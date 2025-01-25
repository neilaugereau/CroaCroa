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
    [SerializeField]
    private PlayerControllerSettings _settingsSO;
    private Rigidbody2D rb;
    private Collider2D collider2d;
    public LayerMask groundLayer;
    [SerializeField]


    private Vector2 move;

    private enum JumpState {CantJump, CanJump, Jumped, Jumping}
    private enum DashState {CantDash, CanDash, Dashing, ReloadDash}

    [SerializeField]
    private JumpState _jumpState = JumpState.CanJump;
    private bool _isGrounded;

    [SerializeField]
    private DashState _dashState = DashState.CanDash;

    private IEnumerator Dashed(bool floorDash)
    {
        yield return new WaitForSeconds(_settingsSO.DashDuration);
        rb.linearVelocityX = 0f;
        _dashState = DashState.CantDash;
        if(floorDash)
        {
            _dashState = DashState.ReloadDash;
            yield return new WaitForSeconds(_settingsSO.FloorDashCooldown - _settingsSO.DashDuration);
            _dashState = DashState.CanDash;
        }
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
        if (_dashState == DashState.CantDash && _isGrounded)
            _dashState = DashState.CanDash;

        rb.gravityScale = _dashState == DashState.Dashing ? _settingsSO.DashAirGravityScale : _settingsSO.MovingGravityScale;
        transform.Translate(move * _settingsSO.Speed * (_jumpState == JumpState.Jumping ? _settingsSO.AirSpeedCoef : 1f) * Time.deltaTime);
        JumpBehaviour();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputMove = context.action.ReadValue<Vector2>().normalized;
        move = new Vector2(inputMove.x, move.y);
        if(move.x != 0f && _dashState != DashState.Dashing)
            transform.localScale = new Vector2(move.x < 0f ? -1 : 1, transform.localScale.y);
    }

    private void JumpBehaviour()
    {
        _isGrounded = IsGrounded();
        switch (_jumpState)
        {
            case JumpState.Jumped:
                if(!_isGrounded) 
                    _jumpState = JumpState.Jumping;
                break;

            case JumpState.Jumping:
                if (_isGrounded)
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
        if (_isGrounded && _jumpState == JumpState.CanJump)
        {
            _jumpState = JumpState.Jumped;
            rb.linearVelocityY += _settingsSO.JumpForce;
            //Debug.Log($"{name} jumped");
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (_dashState == DashState.CanDash)
        {
            _dashState = DashState.Dashing;
            StartCoroutine(Dashed(_isGrounded));
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
