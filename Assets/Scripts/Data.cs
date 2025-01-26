using UnityEngine;

public class Data : MonoBehaviour
{
    
    public BubbleType playerOneWeapon;
    public BubbleType playerTwoWeapon;
    public int playerOneSkinID;
    public int playerTwoSkinID;
    public int player1Score;
    public int player2Score;
    public int player1TrappedCount;
    public int player2TrappedCount;
    public float fightCountdown;
    public bool isFightActive = false;

    public static Data instance;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
