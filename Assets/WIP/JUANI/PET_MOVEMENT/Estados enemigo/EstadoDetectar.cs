using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

/* En este estado el jugador pierde una vida y vuelve al checkpoint cuando el 
   enemigo lo descubre despu�s de 2 segundos */

public class EstadoDetectar : Estado
{
    public NavMeshAgent agente;
    public Transform jugador;
    public bool accionRealizada = false;
    public Animator animator;
    public float temporizador = 0f;  // Variable para el temporizador
    public float tiempoEspera = 2f;
    private AudioSource Barking, Smelling, Walking, Gasping;

    public EstadoDetectar(NavMeshAgent agente, Animator animator, AudioSource Barking, AudioSource Smelling, AudioSource Walking, AudioSource Gasping)
    {
        this.agente = agente;
        this.animator = animator;
        this.Walking = Walking;
        this.Gasping = Gasping;
        this.Barking = Barking;
        this.Smelling = Smelling;
    }

    public override void HacerAccion()
    {
        if (!accionRealizada) { /* Si la acci�n ya se hizo una vez, que no se haga muchas veces m�s*/
            agente.isStopped = true;
            animator.SetInteger("Cambios", 2);
            Barking.Play();
            Walking.Stop();
            Gasping.Stop();
            Smelling.Stop();
            temporizador += Time.deltaTime;  

            if (temporizador >= tiempoEspera)  /* Verificar si han pasado 2 segundos */
            {
                GameManager.instance.playerStats.RemoveVidas(1);
                accionRealizada = true;  /* La acci�n ya se realiz� una vez*/
                agente.isStopped = false;
                Debug.Log("Acci�n realizada despu�s de esperar.");
            }
        }
    }

    /* Se reinicia la acci�n*/
    public void ResetearAccion() {
        accionRealizada = false;
        temporizador = 0f;
    }
}
