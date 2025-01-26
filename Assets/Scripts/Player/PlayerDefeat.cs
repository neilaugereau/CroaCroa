using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDefeat : MonoBehaviour
{
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] TMP_Text _winnerText;

    public UnityEvent onDefeat;

    public void Defeat()
    {
        onDefeat.Invoke();

        // _gameOverCanvas.gameObject.SetActive(true);
        // if(GetComponent<PlayerController>().IsPlayerOne)
        //     _winnerText.text = "Player 2 wins !";
        // else
        //     _winnerText.text = "Player 1 wins !";
        // gameObject.SetActive(false);
    }
}
