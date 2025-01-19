using UnityEngine;

/* Esta condici�n es cuando el jugador ya ha ido al checkpoint despu�s 
   de ser encontrado por el enemigo y sirve para que el enemigo 
   pueda vovler a los otros estados */

public class CondicionRealizada : Condicion
{
    public EstadoDetectar estadoDetectar;

    public CondicionRealizada(EstadoDetectar estadoDetectar)
    {
        this.estadoDetectar = estadoDetectar;
    }

    public override bool Comprobar()
    {
        return estadoDetectar != null && estadoDetectar.accionRealizada;
    }
}