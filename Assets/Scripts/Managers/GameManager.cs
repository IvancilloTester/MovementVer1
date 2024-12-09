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
    public HUDCanvas hudCanvas;
    [SerializeField]
    private GameState gameState;

    //UI



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

    }

}
