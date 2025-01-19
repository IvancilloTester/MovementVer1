using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera maincamera;


    private void Start()
    {
        maincamera = GameManager.instance.mainCamera;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + maincamera.transform.rotation * Vector3.forward, maincamera.transform.rotation * Vector3.up);
    }
}
