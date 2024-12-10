using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    //VARIABLES
    [SerializeField]
    public int vidasActuales;
    [SerializeField]
    public float staminaActual;
    [SerializeField]
    public float staminaMax;

    [SerializeField]
    private bool shieldActive;

    public GameObject shieldOBJ;


    private void Start()
    {
        SetVidas(7);
        AddStamina(staminaMax);
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
    }

    private void CambiarVidas(int _amount)
    {
        vidasActuales+= _amount;
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

    private void CambiarStamina(float _amount)
    {
        staminaActual += _amount;
        GameManager.instance.hudCanvas.UpdateStaminaFill(staminaActual,staminaMax);
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

}
