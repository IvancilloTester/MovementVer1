using UnityEngine;

public class TriggerWater : MonoBehaviour
{

    public int vidasAQuitar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerStats.RemoveVidas(vidasAQuitar);
        }
    }
}
