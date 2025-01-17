using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{

    [SerializeField]
    private Material onMaterial;
    [SerializeField]
    private GameObject buttonObject;
    [SerializeField]
    private float _speed = 2f; // Velocidad del movimiento.
    [SerializeField]
    private float _pushDistance = 0.1f; // Distancia que se empuja el boton

    private Vector3 _pushedPos; // Posición del botón presionado
    private bool _isPushed = false; // Indica si el botón ha sido presionado 

    // Interfaz para hacer un objeto "interactuable" (Puertas o puentes)
    public interface IInteractable
    {
        void Interact();
    }

    [SerializeField]
    private MonoBehaviour interactableObject; // Obtenemos el objeto interactuable para que el boton sepa que funcion ejecutar

    private IInteractable interactable;

    private void Start()
    {
        interactable = interactableObject as IInteractable;
        if (interactable == null) // Nos aseguramos de que el objeto sea "Interactuable"
        {
            Debug.LogError("El objeto asignado no implementa Button.IInteractable.");
        }
    }

    // Cuando el player presiona el botón se llama la función interact, cada objeto "interactuable" tiene su propia implementación
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactable?.Interact();
            if (!_isPushed){ // Nos aseguramos de que el boton no haya sido presionado aún.
                PushButton();
            }
            
        }
    }

    private void PushButton()
    {
        _pushedPos = buttonObject.transform.position - new Vector3(0, _pushDistance, 0); // Se calcula la nueva distancia del boton
        buttonObject.transform.position = Vector3.MoveTowards(buttonObject.transform.position, _pushedPos, _speed * Time.deltaTime); // Se actualiza la posición del botón
        buttonObject.GetComponent<MeshRenderer>().material = onMaterial; 
        _isPushed = true; // Se marca que el botón ya fue presionado
    }
}
