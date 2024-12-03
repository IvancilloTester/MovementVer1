using UnityEngine;

public class DamageObject : MonoBehaviour
{
    //VALORES DEL SCORE
    [SerializeField]
    private int damageValue;


    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.playerStats.RemoveVidas(damageValue);
        this.gameObject.SetActive(false);
    }
}
