using UnityEngine;
using UnityEngine.UIElements;

public class TimeObject : MonoBehaviour
{

    //VALORES DEL SCORE
    [SerializeField]
    private float scoreValue;


    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.levelTimer.RemoveTime(scoreValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"-{scoreValue} segundos", "time");
            this.gameObject.SetActive(false);
        }

    }


}
