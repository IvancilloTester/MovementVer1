using UnityEngine;
using UnityEngine.UIElements;

public class ShieldObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.playerStats.ActivarShield();
        GameManager.instance.hudCanvas.popup.ShowPopup($"+1 escudo", "shield");
        this.gameObject.SetActive(false);
    }
}
