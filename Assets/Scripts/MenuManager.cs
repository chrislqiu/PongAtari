using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartSinglePlayer()
    {
        GameManager.Instance.isAI = true; // right paddle = AI
        SceneManager.LoadScene("GameStart"); // load game scene
    }

    public void StartMultiplayer()
    {
        GameManager.Instance.isAI = false; // right paddle = human
        SceneManager.LoadScene("GameStart"); // load game scene
    }
}
