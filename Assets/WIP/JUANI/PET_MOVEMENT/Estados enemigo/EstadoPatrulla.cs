using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using TMPro;

/* En este estado el enemigo está patrullando por el area designada en las posiciones planteadas */

public class EstadoPatrulla : Estado
{
    public NavMeshAgent agente;
    public List<Transform> posiciones = new List<Transform>();
    public int indiceActual = 0;
    public Animator animator;
    private AudioSource Barking, Smelling, Walking, Gasping;
    private TextMeshProUGUI status;
    public bool sonidoPlayed = false;
    public EstadoPatrulla(NavMeshAgent agen, List<Transform> pos, Animator animator, AudioSource Barking,
                          AudioSource Smelling, AudioSource Walking, AudioSource Gasping, TextMeshProUGUI status)
    {
        this.agente = agen;
        this.posiciones = pos;
        this.animator = animator;
        this.Walking = Walking;
        this.Gasping = Gasping;
        this.Barking = Barking;
        this.Smelling = Smelling;
        this.status = status;

        if (posiciones.Count > 0)
        {
            agente.SetDestination(posiciones[0].position); /* Primer destino */
        }
    }

    
    override public void HacerAccion()
    {

        if (!sonidoPlayed)
        {
            
            Walking.Play();
            Gasping.Play();
            Barking.Stop();
            Smelling.Stop();
            sonidoPlayed = true;
        }
        status.text = "...";
        animator.SetInteger("Cambios", 1);
        agente.speed = 1f;
        /* Para verificar que ya se haya llegado a cada destino */
        if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance) {
            
            Debug.Log("Pos:" + posiciones.Count);
            Debug.Log("index:" + indiceActual);

            /* Se selecciona el siguiente destino */
            indiceActual++;

            Debug.Log("index2:" + indiceActual);

            /* Si ya terminó la ruta, que la vuelva a empezar */
            if (indiceActual >= posiciones.Count)
            {
                indiceActual = 0;
            }

            /* Nuevo destino */
            agente.SetDestination(posiciones[indiceActual].position);
            Debug.Log($"Destino alcanzado. Moviendonos a la posiciónn {indiceActual}: {posiciones[indiceActual].position}");
        }
        sonidoPlayed = false;
    }

}
