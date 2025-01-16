using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TriggerBridge : MonoBehaviour, TriggerButton.IInteractable
{
    [Header("Movement options")]
    [SerializeField]
    private float riseDistance = 0.1f; // Distancia que sube el puente
    [SerializeField]
    private float speed = 2f; // Velocidad del movimiento.


    private bool isExtended = false;
    private Vector3 extendedPosition; // Posición del puente cuando está completamente extendido
    private Vector3 retractedPosition; // Posición del puente cuando está escondido


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        retractedPosition = transform.position; // Posicion original del puente cuando esta escondido
        extendedPosition = transform.position + new Vector3(0, riseDistance, 0);// Se obtiene la posición que tendra el puente cuando este extendido
    }

    // Update is called once per frame
    void Update()
    {
        if (isExtended)
        {
            if (transform.position != extendedPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, extendedPosition, speed * Time.deltaTime);
            }

            //Revisamos si el puente ya esta extendido
            if (transform.position == extendedPosition)
            {
                isExtended = false;
            }



        }
    }

    // Implementa la interfaz IInteractable, al oprimir el boton correspondiente el puente se extiende
    public void Interact()
    {
        if (!isExtended) // Revisamos que no este extendido aún
        {
            isExtended = true;
        }
    }
}
