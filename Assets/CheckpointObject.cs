using UnityEngine;

public class CheckpointObject : MonoBehaviour
{
    [SerializeField]
    private int checkpointID;

    public GameObject claimedIndicator;

    public void ClaimCheckpoint()
    {
        Debug.Log("Claimed checkpoint " + checkpointID);
        claimedIndicator.SetActive(true);
    }

    public void ReleaseCheckpoint()
    {
        Debug.Log("Released checkpoint " + checkpointID);
        claimedIndicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.ChangeCheckpoint(this);
    }
}
