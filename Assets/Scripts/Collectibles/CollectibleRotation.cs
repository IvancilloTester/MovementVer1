using Unity.VisualScripting;
using UnityEngine;

public class CollectibleRotation : MonoBehaviour
{

    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime,0 );
    }

}
