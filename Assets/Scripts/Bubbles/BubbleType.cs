using UnityEngine;

[CreateAssetMenu(fileName = "BubbleType", menuName = "Bubbles/BubbleType")]
public class BubbleType : ScriptableObject
{
    public string description;
    public GameObject prefab;
    public Vector2 sizeThreshold;
    public Vector2 gravityThreshold;
    public Vector2 dispersionThreshold;
    public float shootForce = 10f; 
    public float fireRate = 0.2f;  
    public int burstCount = 10;
}
