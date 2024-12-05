using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public GameObject cam;
    //public GameObject CameraPosition;
    //private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float speed = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 0.03f));
            speed = 0.5f; // Walking speed
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -0.03f));
            speed = 0.5f; // Walking speed
        }

        if (Input.GetKey(KeyCode.LeftShift) && speed > 0)
        {
            speed = 1.5f; // Running speed
        }

        if (Input.GetKey(KeyCode.D))
        {
            CameraPosition.transform.SetParent(null);
            transform.Rotate(new Vector3(0, 0.8f, 0));
            CameraPosition.transform.SetParent(transform);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            CameraPosition.transform.SetParent(null);
            transform.Rotate(new Vector3(0, -0.8f, 0));
            CameraPosition.transform.SetParent(transform);
        }

        // Camera soft movement
        cam.transform.position = Vector3.Lerp(cam.transform.position, CameraPosition.transform.position, 0.01f);

        // Speed parameter animator
        animator.SetFloat("Speed", speed);
        */
    }
}
