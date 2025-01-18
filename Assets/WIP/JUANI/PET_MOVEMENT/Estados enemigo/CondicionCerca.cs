using UnityEngine;

public class CondicionCerca : Condicion
{
    public float distancia;

    public Transform objetivo, transforms;

    public CondicionCerca(float distancia, Transform objeto, Transform transforms) { 
        this.distancia = distancia;
        this.objetivo = objeto;
        this.transforms = transforms;
    }

    public override bool Comprobar() {
        return Vector3.Distance(objetivo.position, transforms.position) < distancia;
    }
}
