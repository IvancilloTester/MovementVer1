using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Componentes
    [SerializeField]
    public LevelTimer levelTimer;
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
        levelTimer.RestartTimer(false);
        playerStats.DesactivarShield();
        petController.transform.position = playerSpawn.position;

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

}
