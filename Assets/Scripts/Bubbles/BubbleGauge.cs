using System;
using UnityEngine;
using UnityEngine.Events;

public class BubbleGauge : MonoBehaviour
{
    private float _gauge;
    public bool isBubbled;

    public UnityEvent onBubbled;
    public UnityEvent onBubbledOut;
    
    public float Gauge
    {
        get => _gauge;
        private set
        {
            if (value <= 0f && isBubbled)
            {
                _gauge = 0f;
                onBubbledOut.Invoke();
            }
            else if(value >= 100f)
            {
                _gauge = 100f;
                isBubbled = true;
                onBubbled.Invoke();
            }
            else
            {
                _gauge = value;
            }
        }
    }

    public void Init() {
        isBubbled = false;
        Gauge = 0f;
    }

    public void Hit(float damages)
    {
        Gauge += damages;
    }

    public void BubbleSmash()
    {
        isBubbled = false;
        Gauge = 0f;
    }
}
