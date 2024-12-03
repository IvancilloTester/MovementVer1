using UnityEngine;

public class LifeObject : MonoBehaviour
{
    //VALORES DEL SCORE
    [SerializeField]
    private int lifeValue;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.playerStats.AddVidas(lifeValue);
        this.gameObject.SetActive(false);
    }
}
