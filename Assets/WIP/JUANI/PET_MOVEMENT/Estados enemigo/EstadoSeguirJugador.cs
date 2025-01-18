using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EstadoSeguirJugador : Estado
{
    public UnityEngine.AI.NavMeshAgent agente;
    public Transform jugador;

    public EstadoSeguirJugador(UnityEngine.AI.NavMeshAgent agen, Transform jugador)
    {
        agente = agen;
        this.jugador = jugador;
    }

    public override void HacerAccion() {
        // Actualiza continuamente el destino del agente hacia el jugador
        agente.SetDestination(jugador.position);
    }
}
