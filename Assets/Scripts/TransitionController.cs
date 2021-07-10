using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    [SerializeField] Animator animator;
    int sceneIndexToGo;

    internal void startFade(int sceneIndex)
    {
        sceneIndexToGo = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    internal void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneIndexToGo);
    }
}
