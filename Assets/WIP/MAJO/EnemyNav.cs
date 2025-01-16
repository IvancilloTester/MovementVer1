using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyNav : MonoBehaviour
{
    /* Inicializar variables*/
    public Animator animator;
    public EstadosNav estado;
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

    /* Si el enemigo se encuentra en estado de idle, realiza los movimientos que correspondan.
       Adem�s, si encuentra con una pared, este va a rotar para no quedarse caminando contra
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
       caminar y rotar. La acci�n que haya depender� del n�mero al azar que 
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
       la condici�n para rotar se cumple*/
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
    public void CambioEstado(EstadosNav e) {
        switch (e)
        {
            case EstadosNav.idle:
                break;
            case EstadosNav.distraido:
                break;
            case EstadosNav.muerto:
                break;
            default:
                break;
        }
        estado = e;
    }

    /* Checkea el estado acual en que nos encontramos */
    private void CheckEstado() {
        switch (estado) {
            case EstadosNav.idle:
                IdleEstado();
                break;
            case EstadosNav.distraido:
                DistraidoEstado();
                break;
            case EstadosNav.muerto:
                MuertoEstado();
                break;
            default:
                break;
        }
    }

    /* Hace la animaci�n del estado idle del enemigo, adem�s si el
       personaje entra en el �rea del enemigo, este hace una animaci�n.
       Despu�s de un waitTime, te quita una vida y regresas al checkpoint */
    public virtual void IdleEstado() {
        animator.SetInteger("Cambios", (int)estado);
        dangerZone.SetActive(true);
        if (distancia <= distanciaVer) {
            CambioEstado(EstadosNav.muerto);
            Barking.Play();
            Gasping.Stop();
            Walking.Stop();
            animator.SetInteger("Cambios", (int)estado);
            Invoke("AccionEnemigo", waitTime);
        }
    }

    /* Hace la animaci�n cuando el personaje se distrae con el huesito */
    public virtual void DistraidoEstado() {
        dangerZone.SetActive(false);
        Gasping.Stop();
        Walking.Stop();
        animator.SetInteger("Cambios", 0);
    }

    /* Si el personaje sale del rango del enemigo, este vuelve al estado Idle */
    public virtual void MuertoEstado() {
        if (distancia > distanciaVer) {
            CambioEstado(EstadosNav.idle);
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
            CambioEstado(EstadosNav.distraido);
            Destroy(Huesito.gameObject);
            Invoke("CambiarIdle", distraidoTime);
        }
    }

    /* Esto lo puse para poder cambiar al estado Idle con un inoke*/
    private void CambiarIdle() {
        CambioEstado(EstadosNav.idle);
        Smelling.Stop();
    }

    /* Remueve una vida del personaje y lo devuelve a un checkpoint 
       la puse as� porque us� invoke para esperar un waitTime antes 
       de realizar esta acci�n */
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

/* Solo se ve en el editor, y es el �rea en que el enemigo te descubre */
#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaVer);
    }
#endif

}

/* Se inicializan los EstadosNav en los que puede estar el enemigo */
public enum EstadosNav { 
    idle        = 0,
    distraido   = 1,
    muerto      = 2
}
