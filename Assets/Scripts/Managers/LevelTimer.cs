using System;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{

    public float timePassed = 0;
    public bool isRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RestartTimer(false);
    }

    void Update()
    {
        AddTime();
    }

    public void StartTimer()
    {
        isRunning=true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void RestartTimer(bool stopped)
    {
        //REGRESAMOS EL TIEMPO A CERO
        timePassed = 0;

        //DEPENDIENDO DE LO QUE NECESITAMOS, HACEMOS QUE INICIE AUTOMATICAMENTE O NO
        if (stopped)
        {
            isRunning = false;
        }
        else
        {
            isRunning = true;
        }

        GameManager.instance.hudCanvas.UpdateTimerTEXT(timePassed);
        
    }
    private void AddTime()
    {

        //SI ISRUNNING ES VERDADERO, AGREGAMOS EL TIEMPO QUE PASA ENTRE CADA FRAME
        if (isRunning)
        {
            timePassed += Time.deltaTime;
            //Debug.Log($"Timer: {timePassed}");
        }
        GameManager.instance.hudCanvas.UpdateTimerTEXT(timePassed);
    }

}
