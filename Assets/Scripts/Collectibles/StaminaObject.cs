using UnityEngine;

public class StaminaObject : MonoBehaviour
{
    [SerializeField]
    private int staminaValue;
    public ParticleSystem particles;
    public GameObject mesh;

    private void OnTriggerEnter(Collider other)
    {


        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.AddStamina(staminaValue);
            GameManager.instance.hudCanvas.popup.ShowPopup($"+{staminaValue} stamina", "stamina");
            particles.Play();
            mesh.transform.localScale = Vector3.zero;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
            Invoke("Deactivate",particles.main.duration);
        }


    }
private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

}
