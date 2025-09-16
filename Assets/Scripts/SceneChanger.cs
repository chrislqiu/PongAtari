using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] Animator animator;
    private string sceneToLoad;

    public void FadeToLevel(string sceneName)
    {
        sceneToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
