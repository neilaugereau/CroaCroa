using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField]
    private float fightDuration = 300f;
    [SerializeField]
    private int roundToWinCount = 3;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private Transform player1Spawn;
    [SerializeField]
    private Transform player2Spawn;

    private BubbleType _playerOneWeapon;
    private BubbleType _playerTwoWeapon;
    private int _playerOneSkinID;
    private int _playerTwoSkinID;

    private bool hasPlayer1Scored;
    private bool hasPlayer2Scored;
    private int player1Score;
    private int player2Score;
    private int player1TrappedCount;
    private int player2TrappedCount;

    private bool isFightActive = false;

    [HideInInspector]
    public float fightCountdown;

    void Awake() {
        if(instance == null)
            instance = this;

            // sub to player score event
            // sub to player trapped event
        else
            Debug.LogWarning("Multiple GameManager instances");
    }

    void Update() {
        if(!isFightActive) return;

        fightCountdown -= Time.deltaTime;

        if(hasPlayer1Scored)
            EndRound(true);
        else if(hasPlayer2Scored)
            EndRound(false);
        else if(fightCountdown <= 0f)
            EndRound(player1TrappedCount >= player2TrappedCount);
    }

    public void LoadData(int skinOneID, BubbleType weaponOne, int skinTwoID, BubbleType weaponTwo)
    {
        _playerOneSkinID = skinOneID;
        _playerTwoSkinID = skinTwoID;
        // @todo Load right animations depending of the skin

        player1.GetComponent<BubbleShooter>().bubbleType = weaponOne;
        player2.GetComponent<BubbleShooter>().bubbleType = weaponTwo;
    }

    public void StartFight() {
        player1Score = 0;
        player2Score = 0;
        player1TrappedCount = 0;
        player2TrappedCount = 0;

        fightCountdown = fightDuration;
        
        StartRound();

        isFightActive = true;
    }

    void StartRound() {
        player1.GetComponent<Transform>().position = player1Spawn.position;
        player2.GetComponent<Transform>().position = player2Spawn.position;

        // player1.GetComponent<BubbleGauge>().Init();
        // player2.GetComponent<BubbleGauge>().Init();
    }

    void EndFight(bool hasPlayer1Won) {
        isFightActive = false;

        // switch to EndScreenScene -> back to Character choice
    }

    void EndRound(bool hasPlayer1Won) {
        player1Score += hasPlayer1Won? 1 : 0;
        player2Score += hasPlayer1Won? 0 : 1;

        if(player1Score == roundToWinCount)
            EndFight(true);
        else if(player2Score == roundToWinCount)
            EndFight(false);
        else
            StartRound();
    }
}
