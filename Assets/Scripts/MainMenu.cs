using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas targetCanvas;
    public SceneChanger sceneChanger;
    [SerializeField] float sceneDelay = 0.5f;
    private Button[] menuButtons;
    void Start()
    {
        if (targetCanvas != null)
        {
            menuButtons = targetCanvas.GetComponentsInChildren<Button>();

            foreach (Button button in menuButtons)
            {
                // Add click listener
                button.onClick.AddListener(() => OnButtonClick(button));

                // Add pointer event
                EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

                var entry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerEnter
                };
                entry.callback.AddListener((data) =>
                {
                    EventSystem.current.SetSelectedGameObject(button.gameObject);
                });

                trigger.triggers.Add(entry);
            }
        }
        else
        {
            Debug.LogError("Target Canvas is not assigned. Please assign it in the Inspector.");
        }
    }

    /* ------------------------------------- Button Behavior ------------------------------------- */

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

    /* -------------------------------------- Scene Changes -------------------------------------- */

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(sceneDelay);
        sceneChanger.FadeToLevel(sceneName);
    }

    public void NewGame() => StartCoroutine(LoadSceneAfterDelay("Game"));

    public void HighScores() => StartCoroutine(LoadSceneAfterDelay("Scores"));

    public void Credits() => StartCoroutine(LoadSceneAfterDelay("Credits"));

    public void Quit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game 
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
