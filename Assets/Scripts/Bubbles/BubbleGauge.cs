using UnityEngine;
using UnityEngine.Events;

public class BubbleGauge : MonoBehaviour
{
    private float _gauge;
    private float _gaugeThreshold;
    public bool isBubbled;
    
    [SerializeField] Sprite _bubbledSprite;
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
                BubbleSmash();
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
        if (isBubbled)
            return;
        
    }
}
