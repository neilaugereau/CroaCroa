using UnityEngine;

public class BubbleEscape : MonoBehaviour
{
    public int pressesNeeded = 20;
    private int currentPresses = 0;
    private bool isPlayerInside = false;
    private GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;
            player = other.gameObject;
            player.GetComponent<PlayerController>().enabled = false;
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.Space))
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
        player.GetComponent<PlayerController>().enabled = true;
        isPlayerInside = false;
        Destroy(gameObject);
    }
}
