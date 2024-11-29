using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 margen;
    public Transform Objetivo;
    // Update is called once per frame
    void Update()
    {
        transform.position = Objetivo.position + margen;
    }
}
