using UnityEngine;

public class HuesitoObject : MonoBehaviour
{
    [SerializeField]
    private int huesitoValue;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.AddHuesitos(huesitoValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"+{huesitoValue} huesito", "huesito");
            this.gameObject.SetActive(false);
        }

    }
}
