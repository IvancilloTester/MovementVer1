using UnityEngine;

/* Esta condición se realiza para que cuando hayan pasado los 5 segundos
   desde que el enemigo se distrae con el huesito, se pase al estado en
   que patrulla de nuevo*/

public class CondicionPatrulla : Condicion
{
    public EstadoHuesito estadoHuesito;

    public CondicionPatrulla(EstadoHuesito estadoHuesito)
    {
        this.estadoHuesito = estadoHuesito;
    }

    public override bool Comprobar()
    {
        return estadoHuesito.TiempoRestante <= 0;
    }
}
