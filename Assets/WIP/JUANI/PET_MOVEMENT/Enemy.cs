using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour
{
    /* Inicializar variables*/
    public Animator animator;
    public Estados estado;
    public float distanciaVer = 3.0f;
    public bool AutoSelectTarget = true;
    public GameObject dangerZone;

    public Transform Target;
    public float distancia;
    public float waitTime = 2.0f;
    public float distraidoTime = 5.0f;

    public float speedMov = 1.0f;
    public float speedRot = 50.0f;
    public int reactTime = 1;
    private int movement = 0;
    bool wait, rotate, walk;
    public float distance = 2.5f;
    public float distanceHuesito;
    public LayerMask detectableLayers;

    private AudioSource Barking;
    private AudioSource Smelling;
    private AudioSource Walking;
    private AudioSource Gasping;

    private void Start() {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        Barking = audioSources[0];
        Smelling = audioSources[1];
        Walking = audioSources[2];
        Gasping = audioSources[3];
        action();
    }

    /* Hace un target en el jugador y empieza la corutina de calcular la distancia*/
    private void Awake() {
        if (AutoSelectTarget) {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        StartCoroutine(CalcularDistancia());
    }

    /* Si el enemigo se encuentra en estado de NoRango, realiza los movimientos que correspondan.
       Además, si encuentra con una pared, este va a rotar para no quedarse caminando contra
       la pared por un tiempo */
    private void Update()
    {
        if ((int)estado == 0) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance)) {
                if (hit.collider) { 
                    rotate = true;
                    StartCoroutine(RotTime());
                }
            }
            if (walk) {
                animator.SetInteger("Cambios", 1);
                transform.position += (transform.forward * speedMov * Time.deltaTime);
            }
            if (rotate) {
                transform.Rotate(Vector3.up * speedRot * Time.deltaTime);
            }
            if (wait) {
                animator.SetInteger("Cambios", 0);
            }
        }
    }

    /* El enemigo hace movimientos aleatorios que varian entre quedarse quieto,
       caminar y rotar. La acción que haya dependerá del número al azar que 
       salga en random*/
    void action() {
        movement = Random.Range(1, 4);
        if (movement == 1) {
            Walking.Play();
            Gasping.Play();
            wait = false; 
            walk = true;
        }
        if (movement == 2) {
            Walking.Stop();
            Gasping.Play();
            wait = true;
            walk = false;
        }
        if (movement == 3) {
            Walking.Play();
            Gasping.Play();
            rotate = true;
            StartCoroutine(RotTime());
        }
        Invoke("action", reactTime);
    }

    /* El enemigo rota y luego deja de hacerlo, esto solo pasa cuando
       la condición para rotar se cumple*/
    IEnumerator RotTime() {
        animator.SetInteger("Cambios", 1);
        yield return new WaitForSeconds(2f);
        rotate = false;
    }

    /* Revisa constantemente el estado actual en que se encuentra el enemigo*/
    private void LateUpdate() {
        CheckEstado();
    }

    /* Hace el cambio al estado deseado*/
    public void CambioEstado(Estados e) {
        switch (e)
        {
            case Estados.NoRango:
                break;
            case Estados.Distraidohuesito:
                break;
            case Estados.EnRango:
                break;
            default:
                break;
        }
        estado = e;
    }

    /* Checkea el estado acual en que nos encontramos */
    private void CheckEstado() {
        switch (estado) {
            case Estados.NoRango:
                NoRangoEstado();
                break;
            case Estados.Distraidohuesito:
                DistraidohuesitoEstado();
                break;
            case Estados.EnRango:
                EnRangoEstado();
                break;
            default:
                break;
        }
    }

    /* Hace la animación del estado NoRango del enemigo, además si el
       personaje entra en el área del enemigo, este hace una animación.
       Después de un waitTime, te quita una vida y regresas al checkpoint */
    public virtual void NoRangoEstado() {
        animator.SetInteger("Cambios", (int)estado);
        dangerZone.SetActive(true);
        if (distancia <= distanciaVer) {
            CambioEstado(Estados.EnRango);
            Barking.Play();
            Gasping.Stop();
            Walking.Stop();
            animator.SetInteger("Cambios", (int)estado);
            Invoke("AccionEnemigo", waitTime);
        }
    }

    /* Hace la animación cuando el personaje se distrae con el huesito */
    public virtual void DistraidohuesitoEstado() {
        dangerZone.SetActive(false);
        Gasping.Stop();
        Walking.Stop();
        animator.SetInteger("Cambios", 0);
    }

    /* Si el personaje sale del rango del enemigo, este vuelve al estado NoRango */
    public virtual void EnRangoEstado() {
        if (distancia > distanciaVer) {
            CambioEstado(Estados.NoRango);
            Barking.Stop();
        }
        
    }

    /* Cuando se tira el huesito y este colisiona con el enemigo, el enemigo
       se distrae por unos segundos antes de volver a su estado habitual */
    private void OnCollisionEnter(Collision Huesito)
    {
        if (((1 << Huesito.gameObject.layer) & detectableLayers) != 0)
        {
            Smelling.Play();
            Debug.Log("Objeto detectado dentro del rango: " + Huesito.gameObject.name);
            CambioEstado(Estados.Distraidohuesito);
            Destroy(Huesito.gameObject);
            Invoke("CambiarNoRango", distraidoTime);
        }
    }

    /* Esto lo puse para poder cambiar al estado NoRango con un inoke*/
    private void CambiarNoRango() {
        CambioEstado(Estados.NoRango);
        Smelling.Stop();
    }

    /* Remueve una vida del personaje y lo devuelve a un checkpoint 
       la puse así porque usé invoke para esperar un waitTime antes 
       de realizar esta acción */
    private void AccionEnemigo() {
        GameManager.instance.playerStats.RemoveVidas(1);
        Debug.Log("Enemigo");
    }

    /* Calcula la distancia a la que el personaje se encuentra del enemigo */
    IEnumerator CalcularDistancia() {
        while (true) {
            if (Target != null) { 
                distancia = Vector3.Distance(transform.position, Target.position);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

/* Solo se ve en el editor, y es el área en que el enemigo te descubre */
#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaVer);
    }
#endif

}

/* Se inicializan los estados en los que puede estar el enemigo */
public enum Estados { 
    NoRango             = 0,
    Distraidohuesito    = 1,
    EnRango      = 2
}
