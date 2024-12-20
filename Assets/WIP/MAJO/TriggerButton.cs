using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{

    [SerializeField]
    Material onMaterial;
    [SerializeField]
    GameObject buttonObject;


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
            PushButton();
        }
    }

    private void PushButton()
    {
        Transform _buttonPos = buttonObject.transform;
        _buttonPos.position = new Vector3(_buttonPos.position.x, 0.1f, _buttonPos.position.z);
        buttonObject.GetComponent<MeshRenderer>().material = onMaterial;
    }
}
