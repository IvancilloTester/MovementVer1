using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI panelTitle;
    public TextMeshProUGUI actualScore;
    public TextMeshProUGUI highestScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void gameOver()
    {
        panelTitle.text = "GAME OVER :(";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Over panel activado");
        actualScore.text = "NOT SET";
        highestScore.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    public void Ganar()
    {
        panelTitle.text = "YOU WIN! :)";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Win panel activado");
        GetScoreData();
    }

    private void GetScoreData()
    {

        if (GameManager.instance.actualScore > GameManager.instance.highScore)
        {
            GameManager.instance.highScore = GameManager.instance.actualScore;
            PlayerPrefs.SetInt("highscore", GameManager.instance.highScore);
        }
        GameManager.instance.highScore = PlayerPrefs.GetInt("highscore");



        actualScore.text = GameManager.instance.actualScore.ToString();
        highestScore.text = GameManager.instance.highScore.ToString();
    }

    public void Restart()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reiniciando nivel");
    }

    public void MainMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("IVAN");
    }

}
