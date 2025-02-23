using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private GunController gunController;
    private GunRotation gunRotation;

    void Awake()
    {
        Time.timeScale = 1; // Đảm bảo game luôn chạy khi restart

        gunController = FindObjectOfType<GunController>();
        gunRotation = FindObjectOfType<GunRotation>();

        if (gunController != null) gunController.SetPauseState(false);
        if (gunRotation != null) gunRotation.SetPauseState(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        if (gunController != null) gunController.SetPauseState(true);
        if (gunRotation != null) gunRotation.SetPauseState(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        if (gunController != null) gunController.SetPauseState(false);
        if (gunRotation != null) gunRotation.SetPauseState(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        //if (gunController != null) gunController.SetPauseState(false);
        //if (gunRotation != null) gunRotation.SetPauseState(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
