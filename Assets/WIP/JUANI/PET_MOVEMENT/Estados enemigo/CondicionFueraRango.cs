using UnityEngine;

/* Esta condición es cuando el jugador se aleja lo suficiente del enemigo
   y este lo puede dejar de seguir */

public class CondicionFueraRango : Condicion
{
    public float distancia;
    public Transform objetivo, transforms;

    public CondicionFueraRango(float distancia, Transform objetivo, Transform transforms)
    {
        this.distancia = distancia;
        this.objetivo = objetivo;
        this.transforms = transforms;
    }

    public override bool Comprobar()
    {
        // Verifica si el jugador está fuera del rango
        return Vector3.Distance(objetivo.position, transforms.position) >= distancia;
    }

}
