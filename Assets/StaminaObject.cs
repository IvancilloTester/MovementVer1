using UnityEngine;

public class StaminaObject : MonoBehaviour
{
    [SerializeField]
    private int staminaValue;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.playerStats.AddStamina(staminaValue);
        this.gameObject.SetActive(false);
    }
}
