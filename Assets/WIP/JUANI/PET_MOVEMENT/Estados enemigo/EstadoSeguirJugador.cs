using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using TMPro;

/* En este estado en emeigo sigue al jugador cuando este se acerca cierta distancia */

public class EstadoSeguirJugador : Estado
{
    public UnityEngine.AI.NavMeshAgent agente;
    public Transform jugador;
    public Animator animator;
    private AudioSource Barking, Smelling, Walking, Gasping;
    private TextMeshProUGUI status;
    public bool sonidoPlayed = false;
    public EstadoSeguirJugador(UnityEngine.AI.NavMeshAgent agen, Transform jugador, Animator animator, AudioSource Barking,
                               AudioSource Smelling, AudioSource Walking, AudioSource Gasping, TextMeshProUGUI status)
    {
        this.agente = agen;
        this.jugador = jugador;
        this.animator = animator;
        this.Walking = Walking;
        this.Gasping = Gasping;
        this.Barking = Barking;
        this.Smelling = Smelling;
        this.status = status;
    }

    public override void HacerAccion() {

        if (!sonidoPlayed)
        {
            Walking.Play();
            Gasping.Play();
            Barking.Stop();
            Smelling.Stop();
            sonidoPlayed = true;
        }

        status.text = "!";
        animator.SetInteger("Cambios", 1);

        /* Actualiza continuamente el destino del agente hacia el jugador */
        agente.SetDestination(jugador.position);
        sonidoPlayed = false;
    }
}
