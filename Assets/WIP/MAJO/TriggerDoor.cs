using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [Header("Doors Game Objects")]
    [SerializeField]
    private Transform rightDoor;
    [SerializeField]
    private Transform leftDoor;

    [Header("Movement options")]
    [SerializeField]
    private float openDistance = 3f; // Distancia que se moveran las puertas al abrirse
    [SerializeField]
    private float speed = 2f; // Velocidad del movimiento.
    [SerializeField]
    private string axis = "x";


    private Vector3 openRightPosition; // Posición a la que se abrirá la puerta derecha.
    private Vector3 openLeftPosition; // Posición a la que se abrirá la puerta izquierda.
    private Vector3 closedRightPosition; // Posición original de la puerta derecha.
    private Vector3 closedLeftPosition; // Posición original de la puerta izquierda.

    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        closedLeftPosition = leftDoor.position;
        closedRightPosition = rightDoor.position;

        if (axis.ToLower() == "x")
        {
            openLeftPosition = closedLeftPosition + new Vector3(-openDistance, 0, 0);
            openRightPosition = closedRightPosition + new Vector3(openDistance, 0, 0);
        } else if (axis.ToLower() == "y")
        {
            openLeftPosition = closedLeftPosition + new Vector3(0, -openDistance, 0);
            openRightPosition = closedRightPosition + new Vector3(0, openDistance, 0);
        } else
        {
            openLeftPosition = closedLeftPosition + new Vector3(0, 0, -openDistance);
            openRightPosition = closedRightPosition + new Vector3(0, 0, openDistance);
        }
    }

    void Update()
    {
        if (isOpen)
        {
            if (rightDoor.position != openRightPosition)
            {
                rightDoor.position = Vector3.MoveTowards(rightDoor.position, openRightPosition, speed * Time.deltaTime);
            }

            if (leftDoor.position != openLeftPosition)
            {
                leftDoor.position = Vector3.MoveTowards(leftDoor.position, openLeftPosition, speed * Time.deltaTime);
            }

            //Revisamos si ambas puertas ya estan abiertas
            if ((rightDoor.position == openRightPosition) && (leftDoor.position == openLeftPosition))
            {
                isOpen = false;
            }



        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen) //Revisamos que la puerta no haya sido abierta aun
        {
            isOpen = true;
        }
    }

}
