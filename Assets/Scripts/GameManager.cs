using System;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField]
    private float fightDuration = 300f;
    [SerializeField]
    private int roundToWinCount = 3;
    [SerializeField]
    private Object player1;
    [SerializeField]
    private Object player2;
    [SerializeField]
    private Transform player1Spawn;
    [SerializeField]
    private Transform player2Spawn;
    [SerializeField]
    private TMP_Text timerText;

    private bool hasPlayer1Scored;
    private bool hasPlayer2Scored;
    private int player1Score;
    private int player2Score;
    private int player1TrappedCount;
    private int player2TrappedCount;

    private bool isFightActive = false;

    [HideInInspector]
    public float fightCountdown;

    private void Awake() {
        if(instance == null)
            instance = this;

            // sub to player score event
            // sub to player trapped event
        else
            Debug.LogWarning("Multiple GameManager instances");
    }

    private void Start()
    {
        fightCountdown = fightDuration;
        StartFight();
    }

    private void Update() {
        if(!isFightActive) return;

        fightCountdown -= Time.deltaTime;
        timerText.text = Mathf.RoundToInt(fightCountdown).ToString();

        if(hasPlayer1Scored)
            EndRound(true);
        else if(hasPlayer2Scored)
            EndRound(false);
        else if(fightCountdown <= 0f)
            EndRound(player1TrappedCount >= player2TrappedCount);
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
