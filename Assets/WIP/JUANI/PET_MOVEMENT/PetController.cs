using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PetController : MonoBehaviour
{
    /* Inicializa las variables para controlar al personaje
       que la cámara lo siga y hacer las animaciones */
    public CharacterController Controller;
    public Transform cam;
    public Animator animator;

    /* Inicializa las variables para controlar 
       el movimiento del personaje */
    public float speed = 0.5f;
    public float smoothingTime = 0.3f;
    float smoothingVelocity, horizontal, vertical;
    float targetAngle, angle;
    Vector3 moveDir, direction;

    /* Inicializa las variables para controlar 
       el salto del personaje */
    [SerializeField] float jumpspeed = 16f, gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded, IsJumping = false;

    /* Inicializa las variables para controlar 
       la stamina del personaje */
    public float stamina = 5;
    float rechargestamina = 0;

    void Update() {
        /* El personaje corre si se presiona Shift Izquierdo */
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = 1.5f;
            stamina -= Time.deltaTime;
            /* Si se acaba la stamina, deja de correr 
               y se empieza a recargar */
            if (stamina < 0) {
                speed = 0.5f;
                rechargestamina += Time.deltaTime;
                if (rechargestamina > 5) {
                    stamina = 5;
                    rechargestamina = 0;
                }
            }
        } else {
            speed = 0.5f;
        }
        /* Si el personaje está en el piso, puede saltar
           y se decide la animación a realizar */
        isGrounded = Controller.isGrounded;
        if (isGrounded && velocity.y < 0) {
            if (IsJumping) {
                IsJumping = false;
                animator.SetBool("IsJumping", false);
            }
            velocity.y = -2f;
        }
        /* Se encuentran los ejes para el movimiento del personaje */
        animator.SetBool("IsWalking", false);
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized; 
        /* Realiza el movimiento del personaje, hace que el personaje rote dependiendo
           de la dirección del movimiento, también se suaviza el movimiento para que el
           personaje no haga giros bruscos */
        if (direction.magnitude > 0.1f) {
            animator.SetBool("IsWalking", true);
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothingVelocity, smoothingTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDir.normalized * speed * Time.fixedDeltaTime);
        }
        /* Si el personaje está saltando, actualiza la animación*/
        if (!IsJumping && !isGrounded) {
            IsJumping = true;
            animator.SetBool("IsJumping", true);
        }
        /* Realiza el salto del personaje */
        velocity.y += gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
    } 

    void OnJump() {
        /* Cuando el personaje salta, se realiza la animación de salto*/
        if (isGrounded) {
            IsJumping = true;
            animator.SetBool("IsJumping", true);
            velocity.y = Mathf.Sqrt(jumpspeed * -0.2f * gravity);
        }
    }
}
