using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Store whether right paddle should be AI
    public bool isAI = false;

    private void Awake()
    {
        // Make sure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // survive scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
