using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour
{
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

    private void Update()
    {
        if ((int)estado == 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance)) {
                if (hit.collider) { 
                    rotate = true;
                    StartCoroutine(RotTime());
                }
            }
            if (walk)
            {
                animator.SetInteger("Cambios", 1);
                transform.position += (transform.forward * speedMov * Time.deltaTime);
            }
            if (rotate)
            {
                //animator.SetInteger("Cambios", 1);
                transform.Rotate(Vector3.up * speedRot * Time.deltaTime);

            }
            if (wait)
            {
                animator.SetInteger("Cambios", 0);
            }
        }
    }

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

    IEnumerator RotTime() {
        animator.SetInteger("Cambios", 1);
        yield return new WaitForSeconds(2f);
        rotate = false;
    }

    private void LateUpdate() {
        CheckEstado();
    }

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

    /* Hace la animación del estado idle del enemigo, además si el
       personaje entra en el área del enemigo, este hace una animación.
       Después de un waitTime, te quita una vida y regresas al checkpoint */
    public virtual void IdleEstado() {
        animator.SetInteger("Cambios", (int)estado);
        if (distancia <= distanciaVer) {
            CambioEstado(Estados.muerto);
            animator.SetInteger("Cambios", (int)estado);
            Invoke("AccionEnemigo", waitTime);
        }
    }

    public virtual void DistraidoEstado() {
        animator.SetInteger("Cambios", (int)estado);

    }

    /* Si el personaje sale del rango del enemigo, este vuelve al estado Idle */
    public virtual void MuertoEstado() {
        if (distancia > distanciaVer)
        {
            CambioEstado(Estados.idle);
        }
        
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
    idle        = 0,
    distraido   = 1,
    muerto      = 2
}
