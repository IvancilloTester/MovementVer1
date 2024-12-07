using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PetController : MonoBehaviour
{
    public CharacterController Controller;
    public Transform cam;

    public float speed = 0.5f;
    Vector2 moveInput;
    Vector3 movement;
    public float smoothingTime = 0.1f;
    float smoothingVelocity;
    
    [SerializeField] float jumpspeed = 16f;
    [SerializeField] float gravity = -9.81f;
    private Vector3 velocity; 
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    void Update() {

        isGrounded = Controller.isGrounded; 
        if (isGrounded && velocity.y < 0) { 
            velocity.y = -2f; 
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothingVelocity, smoothingTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDir.normalized * speed * Time.fixedDeltaTime);
        }
        velocity.y += gravity * Time.deltaTime * 2;
        Controller.Move(velocity * Time.deltaTime);
    }

    void OnJump() { 
        if (isGrounded) { 
            velocity.y = Mathf.Sqrt(jumpspeed * -0.2f * gravity); 
        } 
    }
}
