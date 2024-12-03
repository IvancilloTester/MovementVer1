using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.playerStats.ActivarShield();
        this.gameObject.SetActive(false);
    }
}
