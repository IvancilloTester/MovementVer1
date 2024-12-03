using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    //VARIABLES
    [SerializeField]
    public int vidasActuales;
    [SerializeField]
    private bool shieldActive;

    public GameObject shieldOBJ;


    private void Start()
    {
        vidasActuales = 7;
    }

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
    }

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
    }

}
