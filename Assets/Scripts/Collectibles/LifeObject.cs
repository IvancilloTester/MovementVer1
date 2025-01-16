using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LifeObject : MonoBehaviour
{
    //VALORES DEL SCORE
    [SerializeField]
    private int lifeValue;
    public ParticleSystem particles;
    public GameObject mesh;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.AddVidas(lifeValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"+{lifeValue} vida", "life");
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