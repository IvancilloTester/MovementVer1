using UnityEngine;

public class DamageObject : MonoBehaviour
{
    //VALORES DEL SCORE
    [SerializeField]
    private int damageValue;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.playerStats.RemoveVidas(damageValue);
        }

    }
}
