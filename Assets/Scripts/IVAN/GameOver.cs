using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void gameOver()
    {

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Restart()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("IVAN");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
