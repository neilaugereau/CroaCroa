using System;
using TMPro;
using UnityEngine;

public class PlayerDefeat : MonoBehaviour
{
    private Canvas _gameOverCanvas;
    private TMP_Text _winnerText;

    private void Start()
    {
        _gameOverCanvas = GameObject.Find("GameOverCanvas").GetComponent<Canvas>();
        _winnerText = GameObject.Find("Canvas/WinnerText").GetComponent<TextMeshPro>();
    }

    public void Defeat()
    {
        _gameOverCanvas.enabled = true;
        if(GetComponent<PlayerController>().IsPlayerOne)
            _winnerText.text = "Player 1 wins !";
        else
            _winnerText.text = "Player 2 wins !";
        Destroy(gameObject);
    }
}
