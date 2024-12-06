using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Componentes
    [SerializeField]
    private LevelTimer levelTimer;
    [SerializeField]
    public PlayerScore playerScore;
    [SerializeField]
    public PlayerStats playerStats;
    [SerializeField]
    private GameState gameState;

    //UI
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI gameStateText;
    public TextMeshProUGUI playerVidaText;

    private void Awake()
    {

        //REVISAMOS QUE NO HAY MAS DE UN MANAGER Y LO INSTANCEAMOS.
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Hay mas de un GameManager");
        }

    }

    private void Start()
    {
        levelTimer.RestartTimer(false);
        playerStats.DesactivarShield();
    }

    private void Update()
    {
        //MUY HORRIBLE, PERO SOLO PARA PROBAR
        timerText.text = levelTimer.FloatToTimeFormat();
        scoreText.text = playerScore.actualScore.ToString();
        highscoreText.text = playerScore.highScore.ToString();
        gameStateText.text = gameState.currentState.ToString();
        playerVidaText.text = playerStats.vidasActuales.ToString();
    }

}
