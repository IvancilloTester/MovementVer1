using UnityEngine;

public class TriggerDoor : MonoBehaviour
{

    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("jugadoor");
            leftDoorAnimator.SetTrigger("Open");
            rightDoorAnimator.SetTrigger("Open");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
