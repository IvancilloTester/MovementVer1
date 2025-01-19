using UnityEngine;

/* Esta condición es cuando el enemigo detecta al personaje para que
   se pase al estado de detección */

public class CondicionDetectar : Condicion
{
    public float distancia;

    public Transform objetivo, transforms;

    public CondicionDetectar(float distancia, Transform objeto, Transform transforms)
    {
        this.distancia = distancia;
        this.objetivo = objeto;
        this.transforms = transforms;
    }

    public override bool Comprobar()
    {
        return Vector3.Distance(objetivo.position, transforms.position) < distancia;
    }
}
