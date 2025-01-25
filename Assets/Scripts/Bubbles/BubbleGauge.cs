using System;
using UnityEngine;
using UnityEngine.Events;

public class BubbleGauge : MonoBehaviour
{
    private float _gauge;
    private float _gaugeThreshold = 0f;
    public bool isBubbled;
    
    public float Gauge
    {
        get => _gauge;
        private set
        {
            if (value < 0f)
            {
                _gauge = 0f;
            }
            else if(value > 100f)
            {
                _gauge = 100f;
                isBubbled = true;
            }
            else
            {
                _gauge = value;
            }
        }
    }

    public void Hit(float damages)
    {
        Gauge += damages;
    }

    public void BubbleSmash()
    {
        isBubbled = false;
        _gauge = _gaugeThreshold;
        
        if (_gaugeThreshold < 0.75f)
            _gaugeThreshold += 0.25f;
        else
            _gaugeThreshold = 0.75f;
    }
}
