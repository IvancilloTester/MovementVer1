using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

/* En este estado en emeigo sigue al jugador cuando este se acerca cierta distancia */

public class EstadoSeguirJugador : Estado
{
    public UnityEngine.AI.NavMeshAgent agente;
    public Transform jugador;
    public Animator animator;
    private AudioSource Barking, Smelling, Walking, Gasping;

    public EstadoSeguirJugador(UnityEngine.AI.NavMeshAgent agen, Transform jugador, Animator animator, AudioSource Barking,
                               AudioSource Smelling, AudioSource Walking, AudioSource Gasping)
    {
        this.agente = agen;
        this.jugador = jugador;
        this.animator = animator;
        this.Walking = Walking;
        this.Gasping = Gasping;
        this.Barking = Barking;
        this.Smelling = Smelling;
    }

    public override void HacerAccion() {
        animator.SetInteger("Cambios", 1);
        Walking.Play();
        Gasping.Play();
        Barking.Stop();
        Smelling.Stop();
        /* Actualiza continuamente el destino del agente hacia el jugador */
        agente.SetDestination(jugador.position);
    }
}
