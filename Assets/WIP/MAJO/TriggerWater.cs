using UnityEngine;

public class TriggerWater : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerStats.RemoveVidas(1);
        }
    }
}
