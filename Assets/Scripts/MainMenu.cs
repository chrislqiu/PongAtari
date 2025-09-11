using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class MainMenu : MonoBehaviour
{
    public Canvas targetCanvas;
    private Button[] menuButtons;
    void Start()
    {
        if (targetCanvas != null)
        {
            menuButtons = targetCanvas.GetComponentsInChildren<Button>();

            foreach (Button button in menuButtons)
            {
                button.onClick.AddListener(() => OnButtonClick(button));
            }
        }
        else
        {
            Debug.LogError("Target Canvas is not assigned. Please assign it in the Inspector.");
        }
    }

    void OnButtonClick(Button button)
    {
        // Reset all button titles
        foreach (Button btn in menuButtons)
        {
            TextMeshProUGUI text = btn.GetComponentInChildren<TextMeshProUGUI>();
            text.text = StripArrows(text.text);
        }

        // Change title of selected button
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = AddArrows(buttonText.text);
    }

    string AddArrows(string title)
    {
        return $"> {StripArrows(title)} <";
    }

    string StripArrows(string title)
    {
        return Regex.Replace(title, @"^\s*>\s*(.*?)\s*<\s*$", "$1");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HighScores()
    {
        SceneManager.LoadScene("Scores");
    }
}
