using UnityEngine;
public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private string pauseMenuShowAnimation = "Show";
    [SerializeField]
    private string pauseMenuHideAnimation = "Hide";
    [SerializeField]
    private Animator pauseMenuAnimator;
    private bool isPaused = false;
    public void PauseGame()
    {
        if (isPaused) return;

        Time.timeScale = 0f;
        pauseMenuAnimator.Play(pauseMenuShowAnimation, 0, 0f);
        isPaused = true;
    }
    public void ResumeGame()
    {
        if (!isPaused) return;

        Time.timeScale = 1f;
        pauseMenuAnimator.Play(pauseMenuHideAnimation, 0, 0f);
        isPaused = false;
    }
}