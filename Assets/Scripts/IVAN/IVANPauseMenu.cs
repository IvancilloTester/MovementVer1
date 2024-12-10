using UnityEngine;
using UnityEngine.SceneManagement;

public class IVANPauseMenu : MonoBehaviour
{

    private bool isPaused;

    [SerializeField] private GameObject PauseButton;

    [SerializeField] private GameObject PauseMenu;

    
    public void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();

            }
            else
            {

                Pause();
            }

        }
        if(Input.GetKey(KeyCode.R))
        {

            Restart();
        }

    }

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

    public void Restart()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reiniciando nivel");
    }
}
