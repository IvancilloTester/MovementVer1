using UnityEngine;

public class LifeObject : MonoBehaviour
{
    //VALORES DEL SCORE
    [SerializeField]
    private int lifeValue;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.AddVidas(lifeValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"+{lifeValue} vida", "life");
            this.gameObject.SetActive(false);
        }
    }
}
