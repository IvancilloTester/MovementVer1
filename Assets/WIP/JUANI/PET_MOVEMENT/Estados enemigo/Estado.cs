using UnityEngine;
using System.Collections.Generic;

/* Define la acci�n a hacer en el estado en que nos encontremos*/

public abstract class Estado
{
    public List<Transicion> transiciones = new List<Transicion>();
    public abstract void HacerAccion();
}
