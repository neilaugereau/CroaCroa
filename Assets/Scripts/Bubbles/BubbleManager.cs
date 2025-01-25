using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleManager : MonoBehaviour
{
    
    [SerializeField] public float lifeTime;
    [SerializeField] public float damage;
    [SerializeField] private BubbleType bubbleType;

    private Rigidbody2D rb;
    
    public GameObject owner;
    
    private float _timer;
    void Start()
    {
        _timer = 0;
        rb = GetComponent<Rigidbody2D>();
        
        int r = Random.Range(0, 2);
        if(r == 0) 
            rb.gravityScale = bubbleType.gravityThreshold.x;
        else
            rb.gravityScale = bubbleType.gravityThreshold.y;
    }
 
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject != owner && !other.gameObject.GetComponent<BubbleGauge>().isBubbled)
        {
            other.gameObject.GetComponent<BubbleGauge>().Hit(damage);
            Destroy(this.gameObject);
        }
    }
}
