using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuOptionHover : MonoBehaviour
{
    public Canvas TargetCanvas;
    public GameObject MenuPanel;
    private Button[] menuButtons;


    void Start()
    {
        if (MenuPanel != null)
        {
            MenuPanel.SetActive(true);
        }

        if (TargetCanvas != null)
        {
            menuButtons = TargetCanvas.GetComponentsInChildren<Button>();

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

        MenuPanel.SetActive(false);
    }

    string AddArrows(string title)
    {
        return $"> {StripArrows(title)} <";
    }
    string StripArrows(string title)
    {
        return Regex.Replace(title, @"^\s*>\s*(.*?)\s*<\s*$", "$1");
    }

}