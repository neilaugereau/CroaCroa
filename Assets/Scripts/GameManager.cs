using System;
using System.Collections.Generic;
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
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _winnerText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _roundWinText;

    [SerializeField] private float fightDuration = 300f;
    [SerializeField] private int roundToWinCount = 3;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField]
    private List<RuntimeAnimatorController> animators;
    [SerializeField]
    private List<BubbleType> bubbles;
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
        if (!Data.instance.isFightActive)
            StartFight();
        else
            LoadRound();
    }

    private void LoadRound()
    {
        LoadData();
        _scoreText.text = $"{Data.instance.player1Score} - {Data.instance.player2Score}";
    }

    private void Update() {
        if(!Data.instance.isFightActive) return;

        Data.instance.fightCountdown -= Time.deltaTime;
        timerText.text = Mathf.Floor(Data.instance.fightCountdown / 60f).ToString() + " : " + Mathf.Floor(Data.instance.fightCountdown % 60f).ToString();

        if(Data.instance.fightCountdown <= 0f)
            EndFight();
    }

    public void LoadData()
    {
        string[] choicesData = PlayerPrefs.GetString("Choices", "0-0-1-1").Split('-');

        int skinOneID = int.Parse(choicesData[0]);
        int weaponOneID = int.Parse(choicesData[1]);
        int skinTwoID = int.Parse(choicesData[2]);
        int weaponTwoID = int.Parse(choicesData[3]);
        Data.instance.playerOneSkinID = skinOneID;
        Data.instance.playerTwoSkinID = skinTwoID;

        player1.GetComponent<Animator>().runtimeAnimatorController = animators[skinOneID];
        player2.GetComponent<Animator>().runtimeAnimatorController = animators[skinTwoID];

        player1.GetComponent<BubbleShooter>().bubbleType = bubbles[weaponOneID];
        player2.GetComponent<BubbleShooter>().bubbleType = bubbles[weaponTwoID];

        _roundWinText.text = $"{roundToWinCount} rounds to win !";
    }

    public void StartFight() {
        Data.instance.player1Score = 0;
        Data.instance.player2Score = 0;
        Data.instance.player1TrappedCount = 0;
        Data.instance.player2TrappedCount = 0;

        Data.instance.fightCountdown = fightDuration;
        
        Data.instance.isFightActive = true;

        LoadData();
    }

    void StartRound() {
        Data.instance.isFightActive = true;
        Data.instance.RoundReset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void EndFight() 
    {
        _gameOverText.text = "END !";
        if(Data.instance.player1Score > Data.instance.player2Score)
        {
            _winnerText.text = "Player 1 wins !";
        }
        else if(Data.instance.player2Score > Data.instance.player1Score)
        {
            _winnerText.text = "Player 2 wins !";

        }
        else if(Data.instance.player1TrappedCount > Data.instance.player2TrappedCount)
        {
            _winnerText.text = "Player 2 wins !";

        }
        else if(Data.instance.player2TrappedCount > Data.instance.player1TrappedCount)
        {
            _winnerText.text = "Player 1 wins !";
        }
        else
        {
            _winnerText.text = "Tie !";
        }
        _gameOverCanvas.gameObject.SetActive(true);
        Invoke("EndMatch", 3f);
    }

    private void EndMatch()
    {
        SceneManager.LoadScene("PlayerSelection");
    }

    void EndRound(bool hasPlayer1Won) {
        if(!Data.instance.isFightActive) return;
        Data.instance.player1Score += hasPlayer1Won? 1 : 0;
        Data.instance.player2Score += hasPlayer1Won? 0 : 1;

        
        _gameOverCanvas.gameObject.SetActive(true);
        _winnerText.text = Data.instance.player1Score.ToString() + " - " + Data.instance.player2Score.ToString();
        _scoreText.text = _winnerText.text;
        gameObject.SetActive(false);

        if (Data.instance.player1Score == roundToWinCount || Data.instance.player2Score == roundToWinCount)
            EndFight();
        else
        {
            Data.instance.isFightActive = false;
            Invoke("StartRound", 3f);
        }
    }
}
