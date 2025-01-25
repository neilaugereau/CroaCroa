using System;
using TMPro;
using UnityEngine;

public class PlayerDefeat : MonoBehaviour
{
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] TMP_Text _winnerText;

    public void Defeat()
    {
        _gameOverCanvas.gameObject.SetActive(true);
        if(GetComponent<PlayerController>().IsPlayerOne)
            _winnerText.text = "Player 2 wins !";
        else
            _winnerText.text = "Player 1 wins !";
        gameObject.SetActive(false);
    }
}
