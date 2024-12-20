using UnityEngine;

public class SwingMovement : MonoBehaviour
{
    private HingeJoint hingeJoint;

    [SerializeField]
    private float maxAngle = 45f; // Ángulo máximo hacia adelante
    [SerializeField]
    private float minAngle = -45f; // Ángulo máximo hacia atrás
    [SerializeField]
    private float motorSpeed = 100f; // Velocidad del motor
    [SerializeField]
    private float motorForce = 10f; // Fuerza del motor
    [SerializeField]
    private bool startForward = true; // Indica si el columpio empieza hacia adelante

    private JointMotor motor;


    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();


        // Configurar el motor del HingeJoint
        ConfigureMotor();
    }

    void ConfigureMotor()
    {
        motor = hingeJoint.motor;
        motor.targetVelocity = startForward ? motorSpeed : -motorSpeed; // Comienza hacia adelante si startForward es true, de lo contrario empieza hacia atras
        motor.force = motorForce;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }

    void Update()
    {
        // Verifica el ángulo actual del HingeJoint
        float currentAngle = hingeJoint.angle;

        // Si alcanza el ángulo máximo o mínimo, cambia la dirección
        if (currentAngle >= maxAngle)
        {
            SetMotorSpeed(-motorSpeed); // Cambia dirección hacia atrás
        }
        else if (currentAngle <= minAngle)
        {
            SetMotorSpeed(motorSpeed); // Cambia dirección hacia adelante
        }
    }

    private void SetMotorSpeed(float speed)
    {
        motor = hingeJoint.motor;
        motor.targetVelocity = speed; // Ajusta la velocidad
        hingeJoint.motor = motor;
    }
}
