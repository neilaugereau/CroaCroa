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

    public void RoundReset()
    {
        player1TrappedCount = 0;
        player2TrappedCount = 0;
    }

    public void MatchReset()
    {
        player1Score = 0;
        player2Score = 0;
        RoundReset();
    }
}
