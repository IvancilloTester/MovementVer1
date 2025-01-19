using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using TMPro;

/* Máquina de estados en la que se realizan todos los cambios de estados y se observa que se cumplan todas las condiciones*/

public class MaquinaEstados : MonoBehaviour
{
    Estado estadoActual;
    public NavMeshAgent agente;
    public List<Transform> posiciones = new List<Transform>();
    public Transform jugador;
    public LayerMask huesitoLayer;
    public GameObject dangerZone;
    public Animator animator;
    private AudioSource Barking, Smelling, Walking, Gasping;
    public TextMeshProUGUI statusText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        Barking = audioSources[0];
        Smelling = audioSources[1];
        Walking = audioSources[2];
        Gasping = audioSources[3];

        List<Transform> listaJugador = new List<Transform>();
        listaJugador.Add(jugador);

        /* ESTADOS */
        EstadoPatrulla patrulla = new EstadoPatrulla(agente, posiciones, animator, Barking, Smelling, Walking, Gasping, statusText);
        EstadoSeguirJugador seguirJugador = new EstadoSeguirJugador(agente, jugador, animator, Barking, Smelling, Walking, Gasping, statusText);
        EstadoDetectar vuelveCheckpoint = new EstadoDetectar(agente, animator, Barking, Smelling, Walking, Gasping, statusText);
        EstadoHuesito interactuarHuesito = new EstadoHuesito(transform, 3f, huesitoLayer, agente, dangerZone, 
                                                             animator, Barking, Smelling, Walking, Gasping, statusText);

        /* CONDICIONES */
        CondicionCerca condicionCerca = new CondicionCerca(3f, jugador, transform);
        CondicionFueraRango condicionFueraRango = new CondicionFueraRango(3, jugador, transform);
        CondicionDetectar condicionDetectar = new CondicionDetectar(1.5f, jugador, transform);
        CondicionRealizada condicionRealizada = new CondicionRealizada(vuelveCheckpoint);
        CondicionHuesito condicionHuesito = new CondicionHuesito(transform, 3f, huesitoLayer);
        CondicionPatrulla condicionRegresoPatrulla = new CondicionPatrulla(interactuarHuesito);

        /* TRANSICIONES */
        Transicion patrullaASeguirJugador = new Transicion(condicionCerca, seguirJugador);
        Transicion seguirJugadorAPatrulla = new Transicion(condicionFueraRango, patrulla);
        Transicion JugadorACheckpoint = new Transicion(condicionDetectar, vuelveCheckpoint);
        Transicion checkpointAPatrulla = new Transicion(condicionRealizada, patrulla);
        Transicion patrullaInteractuarHuesito = new Transicion(condicionHuesito, interactuarHuesito);
        Transicion huesitoAPatrulla = new Transicion(condicionRegresoPatrulla, patrulla);

        /* AÑADIR LAS TRANSICIONES */
        patrulla.transiciones.Add(patrullaASeguirJugador);
        seguirJugador.transiciones.Add(seguirJugadorAPatrulla);
        seguirJugador.transiciones.Add(JugadorACheckpoint);
        vuelveCheckpoint.transiciones.Add(checkpointAPatrulla);
        patrulla.transiciones.Add(patrullaInteractuarHuesito);
        interactuarHuesito.transiciones.Add(huesitoAPatrulla);

        /* DEFINIR EL ESTADO ACTUAL */
        estadoActual = patrulla;
    }


    /* OBSERVAR EL CAMBIO DE LAS TRANSICIONES */
    void Update()
    {
        foreach (Transicion transicion in estadoActual.transiciones)
        {
            if (transicion.condicion.Comprobar())
            {
                if (estadoActual is EstadoDetectar)
                {
                    ((EstadoDetectar)estadoActual).ResetearAccion();
                }
                estadoActual = transicion.siguienteEstado;
            }
        }
        estadoActual.HacerAccion();
    }
}
