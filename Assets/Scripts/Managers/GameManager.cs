using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera mainCamera;

    public int targetFrameRate = 60;

    //Componentes
    [SerializeField]
    public LevelTimer levelTimer;
    [SerializeField]
    public GameOver gameOver;
    [SerializeField]
    public PlayerScore playerScore;
    [SerializeField]
    public PlayerStats playerStats;
    [SerializeField]
    public HUDCanvas hudCanvas;
    [SerializeField]
    private GameState gameState;
    [SerializeField]
    public PetController petController;

    public int actualScore;
    public int highScore;



    //CHECKPOINTS
    public Transform playerSpawn;
    public List<CheckpointObject> checkpointsList;
    public CheckpointObject currentCheckpoint;



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
        //PlayerPrefs.SetInt("highscore", 0);
        levelTimer.RestartTimer(false);
        playerStats.DesactivarShield();
        petController.transform.position = playerSpawn.position;
        highScore = PlayerPrefs.GetInt("highscore");
        mainCamera = Camera.main;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;


}

    public void ChangeCheckpoint(CheckpointObject _newCheckpoint)
    {
        //SI YA SE HA ACTIVADO UN CHECKPOINT ANTES
        if (currentCheckpoint!=null)
        {
            currentCheckpoint.ReleaseCheckpoint();
        }
        currentCheckpoint = _newCheckpoint;
        currentCheckpoint.ClaimCheckpoint();

    }

    public void Ganar()
    {
        gameOver.Ganar();
    }

    public void UpdateScores()
    {
        actualScore = Mathf.CeilToInt(levelTimer.maxTime-levelTimer.timePassed)*5;

    }

}
