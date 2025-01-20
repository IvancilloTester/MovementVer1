using UnityEngine;
using System.Collections.Generic;
using TMPro;

/* En este estado, cuando el enemigo detecta que un huseito entra en su rango, se
   distrae y no detecta al jugador por 5 segundos*/

public class EstadoHuesito : Estado
{
    public GameObject dangerZone;
    public Transform enemigo;
    public float rango;
    public LayerMask huesitoLayer;
    public UnityEngine.AI.NavMeshAgent agent;
    public CondicionHuesito condicionHuesito;
    public Animator animator;
    private AudioSource Barking, Smelling, Walking, Gasping;
    private TextMeshProUGUI status;
    public bool sonidoPlayed = false;

    public float tiempoEsperado = 5.0f;  // Tiempo que el agente espera con el huesito
    public float tiempoRestante;

    public float TiempoRestante { 
        get { 
            return tiempoRestante; 
        } 
    }

    public EstadoHuesito(Transform enemigo, float rango, LayerMask huesitoLayer, UnityEngine.AI.NavMeshAgent agent, 
                         GameObject dangerZone, Animator animator, AudioSource Barking, 
                         AudioSource Smelling, AudioSource Walking, AudioSource Gasping, TextMeshProUGUI status)
    {
        this.enemigo = enemigo;
        this.rango = rango;
        this.huesitoLayer = huesitoLayer;
        this.agent = agent;
        this.dangerZone = dangerZone;
        this.tiempoRestante = tiempoEsperado;
        this.animator = animator;
        this.Walking = Walking;
        this.Gasping = Gasping;
        this.Barking = Barking;
        this.Smelling = Smelling;
        this.condicionHuesito = new CondicionHuesito(enemigo, rango, huesitoLayer);
        this.status = status;
    }

    public override void HacerAccion()
    {

        if (!sonidoPlayed)
        {

            Smelling.Play();
            Walking.Stop();
            Gasping.Stop();
            Barking.Stop();
            sonidoPlayed = true;
        }
        animator.SetInteger("Cambios", 0);
        agent.speed = 1f;
        status.text = "???";
        /* Si la condición se cumple*/
        if (condicionHuesito.Comprobar()) {
            Debug.Log("El enemigo está distraído con el huesito.");
            dangerZone.SetActive(false);
            agent.isStopped = true; /* El enemigo no se mueve */
            tiempoRestante = tiempoEsperado;
        } else if (tiempoRestante > 0) { /* El tiempo en que se distrae */
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0) { /* Cuando se acaba el tiempo */
                dangerZone.SetActive(true);
                agent.isStopped = false;
                Debug.Log("Se esperaron 5 segundos. Regresando a Patrulla.");
                sonidoPlayed = false;
            }
        }
    }
}