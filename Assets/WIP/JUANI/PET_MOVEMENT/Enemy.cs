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

    /* Hace un target en el jugador y empieza la corutina de calcular la distancia*/
    private void Awake() {
        if (AutoSelectTarget) {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        StartCoroutine(CalcularDistancia());
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
