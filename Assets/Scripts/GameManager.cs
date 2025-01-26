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
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private Transform player1Spawn;
    [SerializeField]
    private Transform player2Spawn;
    [SerializeField]
    private TMP_Text timerText;
    [SerializeField]
    private Canvas canvas;

    private BubbleType _playerOneWeapon;
    private BubbleType _playerTwoWeapon;
    private int _playerOneSkinID;
    private int _playerTwoSkinID;
    private int player1Score;
    private int player2Score;
    private int player1TrappedCount;
    private int player2TrappedCount;

    private bool isFightActive = false;

    private float fightCountdown;

    private float timeScale = 1f;

    public float DeltaTime {
        get => timeScale * Time.deltaTime;

        private set => timeScale = value;
    }

    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Debug.LogWarning("Multiple GameManager instances");
    }

    private void OnEnable() {
        player1.GetComponent<BubbleGauge>().onBubbled.AddListener(() => { player1TrappedCount++; });
        player2.GetComponent<BubbleGauge>().onBubbled.AddListener(() => { player2TrappedCount++; });
        player1.GetComponent<PlayerDefeat>().onDefeat.AddListener(() => { EndRound(false); });
        player2.GetComponent<PlayerDefeat>().onDefeat.AddListener(() => { EndRound(true); });
    }

    private void Start()
    {
        StartFight();
    }

    private void Update() {
        if(!isFightActive) return;

        fightCountdown -= Time.deltaTime;
        timerText.text = Mathf.Floor(fightCountdown / 60f).ToString() + " : " + Mathf.Floor(fightCountdown % 60f).ToString();

        if(fightCountdown <= 0f)
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

        player1.GetComponent<BubbleGauge>().Init();
        player2.GetComponent<BubbleGauge>().Init();

        timeScale = 1f;
        // reset bubble
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
            timeScale = 0.5f;
            Invoke("StartRound", 1f);
    }
}
