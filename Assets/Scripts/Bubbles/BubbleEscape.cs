using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleEscape : MonoBehaviour
{
    public int pressesNeeded = 20;
    [SerializeField]private int currentPresses = 0;

    [SerializeField] private GameObject _bubbleBubbled;
    private GameObject _tempBubbleBubbled;
    
    private BubbleGauge bubbleGauge;
    private Animator animator;
    private AudioSource _audioSource;
    
    [SerializeField]
    private AudioClip _bubbledSound;

    [SerializeField]
    private float _mashForce;
    [SerializeField,Tooltip("% en moins multiplié aux nombres de fois emprisonnés")]
    private float _mashMalusCoef;
    
    private void Start()
    {
        bubbleGauge = GetComponent<BubbleGauge>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        GetComponent<BubbleGauge>().onBubbledOut.AddListener(EscapeBubble);
    }

    public void FixedUpdate()
    {
        if (bubbleGauge.isBubbled && _tempBubbleBubbled == null)
        {
            GetComponent<PlayerController>().canMove = false;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            animator.SetBool("isBubbled", true);
            _audioSource.PlayOneShot(_bubbledSound, 0.9f);
            _tempBubbleBubbled = Instantiate(_bubbleBubbled, transform.position, Quaternion.identity, transform );
        }
    }

    public void TryEscape(InputAction.CallbackContext context) 
    {
        if (bubbleGauge.isBubbled && context.performed)
        {
            float mashDamages = _mashForce * (1 - (((GetComponent<PlayerController>().IsPlayerOne ? Data.instance.player1TrappedCount : Data.instance.player2TrappedCount)-1) * _mashMalusCoef / 100));
            Debug.Log(mashDamages);
            GetComponent<BubbleGauge>().Hit(-mashDamages);

            StartCoroutine(GetComponent<ShakeEffect>().Shake(0.1f, 0.1f));
            if(_tempBubbleBubbled != null && currentPresses <= pressesNeeded -1)
                StartCoroutine(_tempBubbleBubbled.GetComponent<ShakeEffect>().Shake(0.1f, 0.1f));
        }
    }

    void EscapeBubble()
    {
        GetComponent<PlayerController>().canMove = true;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        animator.SetBool("isBubbled", false);
        Destroy(_tempBubbleBubbled);
        _tempBubbleBubbled = null;
        bubbleGauge.BubbleSmash();
    }
}
