using UnityEngine;

/* Define las transiciones entre estados de nuestra máquina de estados*/

public class Transicion
{
    public Condicion condicion;
    public Estado siguienteEstado;

    public Transicion( Condicion con, Estado est) { 
        condicion = con;
        siguienteEstado = est;

    }
}
