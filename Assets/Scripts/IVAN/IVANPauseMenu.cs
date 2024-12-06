using UnityEngine;
using UnityEngine.SceneManagement;

public class IVANPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseButton;

    [SerializeField] private GameObject PauseMenu;

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
