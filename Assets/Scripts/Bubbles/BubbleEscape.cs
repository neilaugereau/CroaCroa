using System;
using UnityEngine;

public class BubbleEscape : MonoBehaviour
{
    public int pressesNeeded = 20;
    [SerializeField]private int currentPresses = 0;
    private BubbleGauge bubbleGauge;

    private void Start()
    {
        bubbleGauge = GetComponent<BubbleGauge>();
    }

    public void FixedUpdate()
    {
        if (bubbleGauge.isBubbled)
        {
            GetComponent<PlayerController>().canMove = false;
        }
    }

    void Update()
    {
        if (bubbleGauge.isBubbled && Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
            if (currentPresses >= pressesNeeded)
            {
                EscapeBubble();
            }
        }
    }

    void EscapeBubble()
    {
        GetComponent<PlayerController>().canMove = true;
        bubbleGauge.BubbleSmash();
    }
}
