using UnityEngine;

public class SwingMovement : MonoBehaviour
{
    private HingeJoint _hingeJoint;
    private float _speed = 30f;         // Velocidad del columpio
    private float _angleLimit = 90f;    // Ángulo máximo de oscilación
    private bool _movingForward = true; // Control de la dirección del columpio

    void Start()
    {
        // Obtén el HingeJoint del objeto
        _hingeJoint = GetComponent<HingeJoint>();

        // Configura el motor del HingeJoint
        JointMotor motor = _hingeJoint.motor;
        motor.force = -6f;

        motor.targetVelocity = _speed;
        // Aplica el motor al HingeJoint
        _hingeJoint.motor = motor;
        _hingeJoint.useMotor = true; // Asegúrate de activar el motor
    }

    void Update()
    {
    }
}
