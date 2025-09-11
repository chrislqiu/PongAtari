using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HighScores()
    {
        SceneManager.LoadScene("Scores");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
