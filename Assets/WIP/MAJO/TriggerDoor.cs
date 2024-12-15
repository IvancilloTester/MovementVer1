using System;
using UnityEngine;

public class TriggerDoor : MonoBehaviour, TriggerButton.IInteractable
{

    [Header("Movement options")]
    [SerializeField]
    private float openDistance = 3f; // Distancia que se moveran las puertas al abrirse
    [SerializeField]
    private float speed = 2f; // Velocidad del movimiento.
    [SerializeField]
    private string axis = "x";

    // Game Object de cada puerta
    private Transform rightDoor; 
    private Transform leftDoor;
    private Vector3 openRightPosition; // Posición a la que se abrirá la puerta derecha.
    private Vector3 openLeftPosition; // Posición a la que se abrirá la puerta izquierda.
    private Vector3 closedRightPosition; // Posición original de la puerta derecha.
    private Vector3 closedLeftPosition; // Posición original de la puerta izquierda.

    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // El primer hijo del objeto que contiene este script es la puerta izquierda y el segundo hijo es la puerta derecha
        leftDoor = transform.GetChild(0);
        rightDoor = transform.GetChild(1);
            
        closedLeftPosition = leftDoor.position;
        closedRightPosition = rightDoor.position;

        // Calculamos la posición de las puertas abierta dependiendo del eje en el cual se abren
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

    // Implementa la interfaz IInteractable, al oprimir el boton correspondiente la puerta se abre
    public void Interact()
    {
        if (!isOpen) // Revisamos que la puerta no haya sido abierta aun
        {
            isOpen = true;
        }
    }
}
