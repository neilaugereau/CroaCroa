using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class BubbleShooter : MonoBehaviour
{
    public PlayerController playerController;
    public Transform shootPoint;
    public BubbleType bubbleType;
    
    private bool isShooting = false;
    void Update()
    {

    }

    public void ShootingBurst(InputAction.CallbackContext context)
    {
        if(!isShooting)
            StartCoroutine(ShootBurst());
    }
    private IEnumerator ShootBurst()
    {
        isShooting = true;

        for (int i = 0; i < bubbleType.burstCount; i++)
        {
            // Créer une bulle au point de tir
            GameObject bubble = Instantiate(bubbleType.prefab, shootPoint.position, Quaternion.identity);
            
            float randomSize = Random.Range(bubbleType.sizeThreshold.x, bubbleType.sizeThreshold.y);
            bubble.transform.localScale = bubble.transform.localScale * randomSize;
            
            // Ajouter une force pour lancer la bulle
            Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
            Vector2 randomDir = new Vector2(0 ,Random.Range(bubbleType.dispersionThreshold.x, bubbleType.dispersionThreshold.y) );
            if(playerController.transform.localScale.x > 0f)
                rb.AddForce((Vector2.right +randomDir) * bubbleType.shootForce, ForceMode2D.Impulse);
            else
                rb.AddForce((Vector2.left +randomDir) * bubbleType.shootForce, ForceMode2D.Impulse);
            
            // Attendre avant de tirer la prochaine bulle
            yield return new WaitForSeconds(bubbleType.fireRate);
        }

        isShooting = false;
    }
}