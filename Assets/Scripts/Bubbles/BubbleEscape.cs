using System;
using UnityEngine;

public class BubbleEscape : MonoBehaviour
{
    public int pressesNeeded = 20;
    [SerializeField]private int currentPresses = 0;

    [SerializeField] private GameObject _bubbleBubbled;
    private GameObject _tempBubbleBubbled;
    
    private BubbleGauge bubbleGauge;
    private Animator animator;
    
    private void Start()
    {
        bubbleGauge = GetComponent<BubbleGauge>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (bubbleGauge.isBubbled && _tempBubbleBubbled == null)
        {
            GetComponent<PlayerController>().canMove = false;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            animator.SetBool("isBubbled", true);
            _tempBubbleBubbled = Instantiate(_bubbleBubbled, transform.position, Quaternion.identity, transform );
        }
    }

    void Update()
    {
        if (bubbleGauge.isBubbled && Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
            StartCoroutine(GetComponent<ShakeEffect>().Shake(0.1f, 0.1f));
            if(_tempBubbleBubbled != null && currentPresses <= pressesNeeded -1)
                StartCoroutine(_tempBubbleBubbled.GetComponent<ShakeEffect>().Shake(0.1f, 0.1f));
            if (currentPresses >= pressesNeeded)
            {
                EscapeBubble();
            }
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
