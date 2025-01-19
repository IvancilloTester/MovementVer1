using UnityEngine;

/* Esta condición es cuando el enemigo detecta el huesito en su rango
   para así ir al estado en que se distrae con el mismo */

public class CondicionHuesito : Condicion
{
    public Transform enemigo;
    public float rango;  // Rango de detección
    public LayerMask huesitoLayer;  // Capa del huesito para detección

    public CondicionHuesito(Transform enemigo, float rango, LayerMask huesitoLayer)
    {
        this.enemigo = enemigo;
        this.rango = rango;   
        this.huesitoLayer = huesitoLayer;
    }

    public override bool Comprobar()
    {
        // Llamamos a DetectarHuesito y devolvemos el resultado
        return DetectarHuesito();
    }

    // Detecta el huesito dentro del rango
    private bool DetectarHuesito()
    {
        Collider[] hitColliders = Physics.OverlapSphere(enemigo.position, rango, huesitoLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("HuesitoTirar"))
            {
                Debug.Log("Huesito detectado dentro del rango");
                return true;
            }
        }

        return false;  // Si no se detecta el Huesito dentro del rango
    }

}