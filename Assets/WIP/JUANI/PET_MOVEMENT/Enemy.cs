using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Estados estado;
    public float distanciaVer = 3;
    public bool AutoSelectTarget = true;

    public Transform Target;
    public float distancia;
    public float waitTime = 2.0f;

    //[SerializeField]
    //private int damageValue;

    private void Awake() {
        if (AutoSelectTarget)
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        StartCoroutine(CalcularDistancia());
    }

    //void Update() {
    //    Animate();
    //}
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

    public virtual void IdleEstado() {
        animator.SetInteger("Cambios", (int)estado);
        if (distancia < distanciaVer) {
            CambioEstado(Estados.muerto);
        }
    }

    public virtual void DistraidoEstado() {
        animator.SetInteger("Cambios", (int)estado);

    }

    public virtual void MuertoEstado() {
        animator.SetInteger("Cambios", (int)estado);
        StartCoroutine(MuertoEstado2());
    }

    private IEnumerator MuertoEstado2() {
        yield return StartCoroutine(Esperar());
        GameManager.instance.playerStats.RemoveVidas(1);
        CambioEstado(Estados.idle);
    }



    IEnumerator Esperar() {
        yield return new WaitForSeconds(waitTime);
    }

    IEnumerator CalcularDistancia() {
        while (true) {
            if (Target != null) { 
                distancia = Vector3.Distance(transform.position, Target.position);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaVer);
    }
#endif

    //private void Animate() { 
    //    animator.SetInteger("Cambios", (int)estado);
    //}
}

public enum Estados { 
    idle        = 0,
    distraido   = 1,
    muerto      = 2
}
