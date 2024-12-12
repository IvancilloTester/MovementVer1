using UnityEngine;
using UnityEngine.UIElements;

public class ShieldObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {


        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.ActivarShield();
            GameManager.instance.hudCanvas.popup.ShowPopup($"+1 escudo", "shield");
            this.gameObject.SetActive(false);
        }
    }
}
