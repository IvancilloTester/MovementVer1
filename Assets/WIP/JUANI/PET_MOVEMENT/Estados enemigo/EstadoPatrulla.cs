using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EstadoPatrulla : Estado
{
    public NavMeshAgent agente;
    public List<Transform> posiciones = new List<Transform>();
    public int indiceActual = 0;
    //public Animator animator;


    public EstadoPatrulla(NavMeshAgent agen, List<Transform> pos)
    {
        agente = agen;
        posiciones = pos;

        if (posiciones.Count > 0)
        {
            agente.SetDestination(posiciones[0].position); // Asignar el primer destino
        }
    }

    
    override public void HacerAccion()
    {
        //animator.SetInteger("Cambios", 1);
        // Verificar si el agente ya lleg� al destino
        if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance) {

            Debug.Log("Pos:" + posiciones.Count);
            Debug.Log("index:" + indiceActual);

            // Incrementar el �ndice para seleccionar el siguiente destino
            indiceActual++;

            Debug.Log("index2:" + indiceActual);

            // Si llegamos al final de la lista, volvemos al inicio
            if (indiceActual >= posiciones.Count)
            {
                indiceActual = 0;
            }

            // Asignar el nuevo destino
            agente.SetDestination(posiciones[indiceActual].position);

            // Debugging para verificar qu� est� sucediendo
            Debug.Log($"Destino alcanzado. Movi�ndonos a la posici�n {indiceActual}: {posiciones[indiceActual].position}");
        }
    }

}
