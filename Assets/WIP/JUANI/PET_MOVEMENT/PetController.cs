using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PetController : MonoBehaviour
{
    CharacterController Controller;
    float speed = 6f;
    Vector2 moveInput;
    Vector3 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    void OnMove(InputValue value) { 
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        movement.x = moveInput.x * speed;
        movement.z = moveInput.y * speed;
        Controller.Move(movement * Time.fixedDeltaTime);
    }
}
