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

    public Transform Target;
    public float distancia;
    public float waitTime = 2.0f;

    public float speedMov = 1.0f;
    public float speedRot = 50.0f;
    public int reactTime = 1;
    private int movement = 0;
    bool wait, rotate, walk;
    public float distance = 2.5f;
    public float distanceHuesito;
    public LayerMask detectableLayers;

    private void Start() {
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
            wait = false; 
            walk = true;
        }
        if (movement == 2) {
            wait = true;
            walk = false;
        }
        if (movement == 3) {
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
    public void CambioEstado(Estados e) {
        switch (e)
        {
            case Estados.idle:
                break;
            case Estados.distraido:
                break;
            case Estados.muerto:
                break;
            default:
                break;
        }
        estado = e;
    }

    /* Checkea el estado acual en que nos encontramos */
    private void CheckEstado() {
        switch (estado) {
            case Estados.idle:
                IdleEstado();
                break;
            case Estados.distraido:
                DistraidoEstado();
                break;
            case Estados.muerto:
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
        if (distancia <= distanciaVer) {
            CambioEstado(Estados.muerto);
            animator.SetInteger("Cambios", (int)estado);
            Invoke("AccionEnemigo", waitTime);
        }
    }

    /* Hace la animaci�n cuando el personaje se distrae con el huesito */
    public virtual void DistraidoEstado() {
        animator.SetInteger("Cambios", 0);
    }

    /* Si el personaje sale del rango del enemigo, este vuelve al estado Idle */
    public virtual void MuertoEstado() {
        if (distancia > distanciaVer)
        {
            CambioEstado(Estados.idle);
        }
        
    }

    /* Cuando se tira el huesito y este colisiona con el enemigo, el enemigo
       se distrae por unos segundos antes de volver a su estado habitual */
    private void OnCollisionEnter(Collision Huesito)
    {
        if (((1 << Huesito.gameObject.layer) & detectableLayers) != 0)
        {
            Debug.Log("Objeto detectado dentro del rango: " + Huesito.gameObject.name);
            CambioEstado(Estados.distraido);
            Destroy(Huesito.gameObject);
            Invoke("CambiarIdle", waitTime * 2);
        }
    }

    /* Esto lo puse para poder cambiar al estado Idle con un inoke*/
    private void CambiarIdle() {
        CambioEstado(Estados.idle);
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

/* Se inicializan los estados en los que puede estar el enemigo */
public enum Estados { 
    idle        = 0,
    distraido   = 1,
    muerto      = 2
}
