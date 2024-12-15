using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    //VARIABLES
    [SerializeField]
    public int vidasActuales;
    public int vidasMax;
    [SerializeField]
    public float staminaActual;
    [SerializeField]
    public float staminaMax;

    [SerializeField]
    public int huesitosActuales;

    [SerializeField]
    private bool shieldActive;

    public GameObject shieldOBJ;
    public GameOver gameOverScript; //añadi esto para el game over screen Ivan


    private void Start()
    {
        SetVidas(vidasMax);
        AddStamina(staminaMax);
        SetHuesitos(0);
        if (gameOverScript == null) 
        { 
            
            Debug.LogError("gameOverScript no ha sido asignado en el Inspector"); 
        
        }
    }

    //VIDAS
    public void AddVidas(int _vidas)
    {

        CambiarVidas(_vidas);
    }

    public void RemoveVidas(int _vidas)
    {
        if(shieldActive)
        {
            DesactivarShield();
            return;
        }
        CambiarVidas(-_vidas);
        if(vidasActuales <= 0)
        {
            gameOverScript.gameOver();
            Debug.Log("Game Over");
        }
        else
        {
            MovePlayerToCheckpoint();

        }
        
    }

    private void MovePlayerToCheckpoint()
    {

        Vector3 _newPosition;
        if (!GameManager.instance.currentCheckpoint)
        {
            _newPosition = GameManager.instance.playerSpawn.transform.position;
            Debug.Log("AL INICIO");
        }
        else
        {
            _newPosition = GameManager.instance.currentCheckpoint.transform.position;
            Debug.Log("AL CHECKPOINT");
        }


        GameManager.instance.petController.Teleport(_newPosition);
        Debug.Log(GameManager.instance.petController.transform.position);


    }


    private void CambiarVidas(int _amount)
    {
        vidasActuales+= _amount;
        if (vidasActuales > vidasMax)
        {
            vidasActuales = vidasMax;
        }
        GameManager.instance.hudCanvas.UpdateVidas(vidasActuales);
    }

    private void SetVidas(int _amount)
    {
        vidasActuales = _amount;
        GameManager.instance.hudCanvas.UpdateVidas(vidasActuales);
    }

    //STAMINA
    public void AddStamina(float _amount)
    {
        CambiarStamina(_amount);
    }

    public void RemoveStamina(float _amount)
    {

        CambiarStamina(-_amount);
    }

    public void CambiarStamina(float _amount)
    {
        staminaActual += _amount;
        GameManager.instance.hudCanvas.UpdateStaminaFill(staminaActual,staminaMax);
        GameManager.instance.petController.stamina=staminaActual;
    }

    public void SetStamina(float _amount)
    {
        staminaActual = _amount;
        GameManager.instance.hudCanvas.UpdateStaminaFill(staminaMax, staminaActual);
    }

    //ESCUDO
    public void ActivarShield()
    {
        ToggleShield(true);
    }

    public void DesactivarShield()
    {
        ToggleShield(false);
    }

    private void ToggleShield(bool _active) {
        shieldActive= _active;
        shieldOBJ.SetActive(_active);
        GameManager.instance.hudCanvas.ToggleShieldIcon(_active);
    }

    //HUESITOS
    public void AddHuesitos(int _amount)
    {
        CambiarHuesitos(_amount);
    }

    public void RemoveHuesitos(int _amount)
    {
        CambiarHuesitos(-_amount);
    }

    private void CambiarHuesitos(int _amount)
    {
        huesitosActuales += _amount;
        GameManager.instance.hudCanvas.UpdateHuesitosTEXT(huesitosActuales);
    }

    private void SetHuesitos(int _amount)
    {
        huesitosActuales = _amount;
        GameManager.instance.hudCanvas.UpdateHuesitosTEXT(huesitosActuales);
    }

}
