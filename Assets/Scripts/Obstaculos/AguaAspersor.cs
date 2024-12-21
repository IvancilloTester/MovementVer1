using UnityEngine;

public class AguaAspersor : MonoBehaviour
{
    [SerializeField]
    public int damageValue;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);

        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.RemoveVidas(damageValue);
        }

    }
}

