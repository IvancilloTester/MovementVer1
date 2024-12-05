using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PetController : MonoBehaviour
{
    CharacterController Controller;
    //Animator animator;
    [SerializeField] float speed = 6f;
    //[SerializeField] float rotspeed = 3f
    Vector2 moveInput;
    Vector3 movement;

    [SerializeField] float jumpspeed = 0.2f;
    [SerializeField] float jumpgravity = 0.75f;
    bool jumping = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
    }

    void OnMove(InputValue value) { 
        moveInput = value.Get<Vector2>();
    }
    void OnJump() {
        if (Controller.isGrounded && !jumping) {
            jumping = true;
        }
    }

    private void FixedUpdate()
    {
        movement.x = moveInput.x * speed;
        movement.z = moveInput.y * speed;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (Controller.isGrounded) {
            movement.y = -jumpgravity * 0.1f;
            if (jumping) {
                movement.y = jumpspeed;
                jumping = false;
            }
        } else { 
            movement.y -= jumpgravity * (movement.y > 0  ? 2.25f : 2.75f);
        }
        //if (direction.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Controller.Move(movement * Time.fixedDeltaTime);
        //}
    }
}
