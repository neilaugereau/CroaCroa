using UnityEngine;

public class BubbleBubbledBounce : MonoBehaviour
{
    public float bounceForce = 15f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null && collision.attachedRigidbody.linearVelocityY < 0f)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
                GetComponentInParent<PlayerDefeat>().Defeat();
            }
        }
    }
}
