using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class ShieldObject : MonoBehaviour
{
    public ParticleSystem particles;
    public GameObject mesh;

    private void OnTriggerEnter(Collider other)
    {


        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.ActivarShield();
            GameManager.instance.hudCanvas.popup.ShowPopup($"+1 escudo", "shield");
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