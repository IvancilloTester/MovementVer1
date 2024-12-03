using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int actualScore;
    public int highScore;

    public void ResetScore()
    {
        actualScore = 0;
    }

    public void AddScore(int _amount)
    {
        actualScore += _amount;

        //REVISAMOS Y ACTUALIZAMOS EL HIGHSCORE
        if(actualScore >= highScore)
        {
            highScore = actualScore;
        }
    }
    public void SubtractScore(int _amount)
    {
        actualScore -= _amount;
    }


}
