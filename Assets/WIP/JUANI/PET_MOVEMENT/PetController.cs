using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PetController : MonoBehaviour
{
    public CharacterController Controller;
    public Transform cam;
    private Animator animator;

    public float speed = 0.5f;
    Vector2 moveInput;
    Vector3 movement;
    public float smoothingTime = 0.1f;
    float smoothingVelocity;

    [SerializeField] float jumpspeed = 16f;
    [SerializeField] float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;
    public bool IsJumping = false;

    public float stamina = 5;
    float rechargestamina = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = 1.5f;
            stamina -= Time.deltaTime;
            if (stamina < 0) {
                speed = 0.5f;
                rechargestamina += Time.deltaTime;
                if (rechargestamina > 5) { 
                    stamina = 5;
                    rechargestamina = 0;
                }
            }
        }
        else {
            speed = 0.5f;
        }
        isGrounded = Controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            if (IsJumping)
            {
                IsJumping = false; 
                animator.SetBool("IsJumping", false);
            }
            velocity.y = -2f;
        }
        animator.SetBool("IsWalking", false);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude > 0.1f)
        {
            animator.SetBool("IsWalking", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothingVelocity, smoothingTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDir.normalized * speed * Time.fixedDeltaTime);
        }
        if (!IsJumping && !isGrounded)
        {
            IsJumping = true;
            animator.SetBool("IsJumping", true); // Actualiza el Animator al saltar
        }
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
    } 

    void OnJump() { 
        if (isGrounded) {
            IsJumping = true;
            animator.SetBool("IsJumping", true);
            velocity.y = Mathf.Sqrt(jumpspeed * -0.2f * gravity);
        }
    }
}
