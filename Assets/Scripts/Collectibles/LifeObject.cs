using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LifeObject : MonoBehaviour
{
    //VALORES DEL SCORE
    [SerializeField]
    private int lifeValue;
    public ParticleSystem particles;
    public GameObject mesh;
    public AudioSource sfx;
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CharacterController>() != null)
        {
            sfx.Play();
            GameManager.instance.playerStats.AddVidas(lifeValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"+{lifeValue} vida", "life");
            particles.Play();
            mesh.transform.localScale = Vector3.zero;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
            Invoke("Deactivate", particles.main.duration);
        }

    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}