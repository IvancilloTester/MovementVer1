using UnityEngine;

public class StaminaObject : MonoBehaviour
{
    [SerializeField]
    private int staminaValue;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.playerStats.AddStamina(staminaValue);
        GameManager.instance.hudCanvas.popup.ShowPopup($"+{staminaValue} stamina", "stamina");
        this.gameObject.SetActive(false);
    }
}
