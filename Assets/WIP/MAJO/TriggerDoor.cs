using UnityEngine;

public class TriggerDoor : MonoBehaviour
{

    public Transform rightDoor;
    public Transform leftDoor;
    public float openDistance = 3f; // Distancia que se moveran las puertas al abrirse
    public float speed = 2f; // Velocidad del movimiento.



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

        openLeftPosition = closedLeftPosition + new Vector3(-openDistance, 0, 0);
        openRightPosition = closedRightPosition + new Vector3(openDistance, 0, 0);
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
