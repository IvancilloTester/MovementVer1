using UnityEngine;

public class WinObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            GameManager.instance.Ganar();

        }

    }
}
