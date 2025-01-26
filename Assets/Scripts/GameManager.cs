using System;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private TMP_Text _winnerText;

    [SerializeField] private float fightDuration = 300f;
    [SerializeField] private int roundToWinCount = 3;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private TMP_Text timerText;

    private void Awake() {
        if(instance == null) {
            instance = this;
            
            player1.GetComponent<BubbleGauge>().onBubbled.AddListener(() => { Data.instance.player1TrappedCount++; });
            player2.GetComponent<BubbleGauge>().onBubbled.AddListener(() => { Data.instance.player2TrappedCount++; });
            player1.GetComponent<PlayerDefeat>().onDefeat.AddListener(() => { EndRound(false); });
            player2.GetComponent<PlayerDefeat>().onDefeat.AddListener(() => { EndRound(true); });
        }
        else
            Debug.LogWarning("Multiple GameManager instances");
            
    }

    private void Start() {
        if(!Data.instance.isFightActive)
            StartFight();
    }

    private void Update() {
        if(!Data.instance.isFightActive) return;

        Data.instance.fightCountdown -= Time.deltaTime;
        timerText.text = Mathf.Floor(Data.instance.fightCountdown / 60f).ToString() + " : " + Mathf.Floor(Data.instance.fightCountdown % 60f).ToString();

        if(Data.instance.fightCountdown <= 0f)
            EndFight(Data.instance.player1TrappedCount >= Data.instance.player2TrappedCount);
    }

    public void LoadData(int skinOneID, BubbleType weaponOne, int skinTwoID, BubbleType weaponTwo)
    {
        Data.instance.playerOneSkinID = skinOneID;
        Data.instance.playerTwoSkinID = skinTwoID;
        // @todo Load right animations depending of the skin

        player1.GetComponent<BubbleShooter>().bubbleType = weaponOne;
        player2.GetComponent<BubbleShooter>().bubbleType = weaponTwo;
    }

    public void StartFight() {
        Data.instance.player1Score = 0;
        Data.instance.player2Score = 0;
        Data.instance.player1TrappedCount = 0;
        Data.instance.player2TrappedCount = 0;

        Data.instance.fightCountdown = fightDuration;
        
        Data.instance.isFightActive = true;
    }

    void StartRound() {
        Data.instance.isFightActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void EndFight(bool hasPlayer1Won) {
        Data.instance.isFightActive = false;

        // switch to EndScreenScene -> back to Character choice
    }

    void EndRound(bool hasPlayer1Won) {
        if(!Data.instance.isFightActive) return;
        Data.instance.player1Score += hasPlayer1Won? 1 : 0;
        Data.instance.player2Score += hasPlayer1Won? 0 : 1;

        
        _gameOverCanvas.gameObject.SetActive(true);
        _winnerText.text = Data.instance.player1Score.ToString() + " - " + Data.instance.player2Score.ToString();
        gameObject.SetActive(false);

        if(Data.instance.player1Score == roundToWinCount)
            EndFight(true);
        else if(Data.instance.player2Score == roundToWinCount)
            EndFight(false);
        else {
            Data.instance.isFightActive = false;
            Invoke("StartRound", 3f);
        }
    }
}
