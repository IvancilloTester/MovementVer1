using UnityEngine;

public class StaminaObject : MonoBehaviour
{
    [SerializeField]
    private int staminaValue;

    private void OnTriggerEnter(Collider other)
    {


        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.AddStamina(staminaValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"+{staminaValue} stamina", "stamina");
            this.gameObject.SetActive(false);
        }
    }
}
