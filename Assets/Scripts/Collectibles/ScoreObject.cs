using UnityEngine;

public class ScoreObject : MonoBehaviour
{

    //VALORES DEL SCORE
    [SerializeField]
    private int scoreValue;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.playerScore.AddScore(scoreValue);
        this.gameObject.SetActive(false);
    }


}
