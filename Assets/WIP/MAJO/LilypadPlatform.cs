using UnityEngine;

public class LilypadPlatform : MonoBehaviour
{
    [Header("Platform Timers")]
    [SerializeField]
    private float timeToDestroy = 3f;// Tiempo que tiene el jugador antes de que la plataforma desaparezca, debe ser mayor al tiempo del parpadeo
    [SerializeField]
    private float timeToReappear = 4f;// Tiempo que tarda la plataforma en reaparecer
    [SerializeField]

    [Header("Blinking Timers")]
    private float blinkingInterval = 0.5f; // Intervalo de tiempo en el que se repite el parpadeo
    [SerializeField]
    private float blinkingTime = 2.5f; // Tiempo del parpadeo


    private bool isBlinking = false;
    public MeshRenderer meshRenderer;
    private Collider platformCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformCollider = GetComponent<MeshCollider>();
    }

    // Hace la plataforma visible e invisible para simular un parpadeo
    void Blinking()
    {
        meshRenderer.enabled = !meshRenderer.enabled; // Alterna entre visible e invisible
    }

    // Se llama cuando se requiere detener el parpadeo
    void StopBlinking()
    {
        CancelInvoke(nameof(Blinking)); // Detiene el parpadeo
        meshRenderer.enabled = true; // Nos aseguramos de que sea visible antes de desaparecer
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER");
        if (other.gameObject.CompareTag("Player") &!isBlinking)
        {
            isBlinking = true;
            InvokeRepeating(nameof(Blinking), 0f, blinkingInterval); // Se empieza el parpadeo llamando repetidamente la funcion Blinking desde el inicio en intervalors de blinkingInterval segundos.
            Invoke(nameof(StopBlinking), blinkingTime); // Se detiene el parpadeo despues de cierto tiempo (blinkingTime).
            Invoke(nameof(PlatformDisappears), timeToDestroy); // La plataforma desaparece despues de cierto tiempo (timeToDestroy), ocurre al mismo tiempo que el parpadeo
        }
    }

    // Desactiva el mesh renderer y el collider de la plataforma
    void PlatformDisappears()
    {
        meshRenderer.enabled = false;
        platformCollider.enabled = false;

        Invoke(nameof(PlatformAppears), timeToReappear); // La plataforma regresa a su estado normal despues de timeToReappear segundos.
    }


    // Reactiva el mesh renderer y el collider de la plataforma.
    void PlatformAppears()
    {
        isBlinking = false;
        meshRenderer.enabled = true;
        platformCollider.enabled = true;
    }
}
