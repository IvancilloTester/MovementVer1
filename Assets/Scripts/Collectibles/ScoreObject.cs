using UnityEngine;

public class ScoreObject : MonoBehaviour
{

    //VALORES DEL SCORE
    [SerializeField]
    private float scoreValue;


    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.levelTimer.RemoveTime(scoreValue);
        this.gameObject.SetActive(false);
    }


}
