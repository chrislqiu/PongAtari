using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    public GameObject MenuPanel;
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        if (MenuPanel.activeSelf == false)
        {
            GetComponent<Canvas>().enabled = true;
        }
    }
}
