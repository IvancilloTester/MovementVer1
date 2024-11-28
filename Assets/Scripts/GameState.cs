using UnityEngine;


//ESTABLECEMOS LOS ESTADOS POSIBLES
public enum GameStates
{
    MainMenu,
    Paused,
    Playing,
    GameOver

}



public class GameState : MonoBehaviour
{

    public GameStates currentState;

    private void Start()
    {
        currentState = GameStates.Playing;
    }


    //CUANDO ENTRAMOS A UN NUEVO ESTADO
    public void EnterState(GameStates state)
    {
        //SALIMOS DEL ANTERIOR
        ExitState();
        //ESTABLECEMOS LA VARIABLE CURRENT CON LA NUEVA
        currentState = state;
        Debug.Log($"Entered state {currentState}");

    }

    public void ExitState()
    {
        //PODEMOS ANADIR FUNCIONES AQUI ESPECIFICAS PARA CADA ESTADO
        Debug.Log($"Exited state {currentState}");
    }

    //SOLO PARA BOTONES DE PRUEBA
    public void EnterMainMenu()
    {
        EnterState(GameStates.MainMenu);
    }
    public void EnterPause()
    {
        EnterState(GameStates.Paused);
    }
    public void EnterPlay()
    {
        EnterState(GameStates.Playing);
    }

    public void EnterGameOver()
    {
        EnterState(GameStates.GameOver);
    }

}

