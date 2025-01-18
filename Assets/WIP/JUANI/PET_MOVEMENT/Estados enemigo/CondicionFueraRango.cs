using UnityEngine;

public class CondicionFueraRango : Condicion
{
    public float distancia;
    public Transform objetivo;
    public Transform origen;

    public CondicionFueraRango(float distancia, Transform objetivo, Transform origen)
    {
        this.distancia = distancia;
        this.objetivo = objetivo;
        this.origen = origen;
    }

    public override bool Comprobar()
    {
        // Verifica si el jugador está fuera del rango
        return Vector3.Distance(objetivo.position, origen.position) >= distancia;
    }

}
