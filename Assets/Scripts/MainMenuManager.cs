using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayerSelection");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
