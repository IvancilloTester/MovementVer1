using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HuesitoObject : MonoBehaviour
{
    [SerializeField]
    private int huesitoValue;
    public ParticleSystem particles;
    public GameObject mesh;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.AddHuesitos(huesitoValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"+{huesitoValue} huesito", "huesito");
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
