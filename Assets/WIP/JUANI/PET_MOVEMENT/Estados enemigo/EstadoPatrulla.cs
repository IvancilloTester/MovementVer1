using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EstadoPatrulla : Estado
{
    public NavMeshAgent agente;
    public List<Transform> posiciones = new List<Transform>();
    public int indiceActual;


    public EstadoPatrulla(NavMeshAgent agen, List<Transform> pos) { 
        agente = agen;
        posiciones = pos;
    }
    override public void HacerAccion()
    {
        if (Vector3.Distance(agente.transform.position, posiciones[indiceActual].position) < 1)
        {
            indiceActual++;
            if (indiceActual >= posiciones.Count)
            {
                indiceActual = 0;
            }
            else
            {
                agente.SetDestination(posiciones[indiceActual].position);
            }
        }
    }

}
