using UnityEngine;
using UnityEngine.Events;

public class BubbleGauge : MonoBehaviour
{
    private float _gauge;
    public float Gauge
    {
        get => _gauge;
        private set
        {
            if (value < 0f)
            {
                // @todo g�rer la sorti de mash
                _gauge = 0f;
            }
            else if(value > 100f)
            {
                _gauge = 100f;
                // @todo g�rer l'entr�e en �tat de bulle
                Debug.Log("Bull(i)ed !!");
            }
            else
            {
                _gauge = value;
            }
            OnGaugeChange.Invoke(Gauge, GetComponent<PlayerController>().IsPlayerOne);
        }
    }

    public UnityEvent<float, bool> OnGaugeChange;

    public void Hit(float damages)
    {
        Gauge += damages;
    }

    public void Update()
    {
        Hit(Random.Range(0f,0.05f));
    }
}
