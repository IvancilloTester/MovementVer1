using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class MaquinaEstados : MonoBehaviour
{
    Estado estadoActual;
    public NavMeshAgent agente;
    public List<Transform> posiciones = new List<Transform>();
    public Transform jugador;
    //public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Transform> listaJugador = new List<Transform>();
        listaJugador.Add(jugador);
        EstadoPatrulla patrulla = new EstadoPatrulla(agente, posiciones);
        EstadoSeguirJugador seguirJugador = new EstadoSeguirJugador(agente, jugador);

        CondicionCerca condicionCerca = new CondicionCerca(5, jugador, transform);
        CondicionFueraRango condicionFueraRango = new CondicionFueraRango(5, jugador, transform);

        Transicion patrullaASeguirJugador = new Transicion(condicionCerca, seguirJugador);
        Transicion seguirJugadorAPatrulla = new Transicion(condicionFueraRango, patrulla);

        patrulla.transiciones.Add(patrullaASeguirJugador);
        seguirJugador.transiciones.Add(seguirJugadorAPatrulla);

        estadoActual = patrulla;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transicion transicion in estadoActual.transiciones)
        {
            if (transicion.condicion.Comprobar())
            {
                estadoActual = transicion.siguienteEstado;
            }
        }
        estadoActual.HacerAccion();
    }
}
