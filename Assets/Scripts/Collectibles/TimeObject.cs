using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class TimeObject : MonoBehaviour
{

    //VALORES DEL SCORE
    [SerializeField]
    private float scoreValue;
    public ParticleSystem particles;
    public GameObject mesh;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.levelTimer.RemoveTime(scoreValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"-{scoreValue} segundos", "time");
            particles.Play();
            mesh.transform.localScale = Vector3.zero;
            Invoke("Deactivate", particles.main.duration);
        }

    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}