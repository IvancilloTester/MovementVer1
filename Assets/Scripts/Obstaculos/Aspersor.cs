using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Aspersor : MonoBehaviour
{
    [SerializeField]
    float intervaloDisparos;

    public int damageValue;

    [SerializeField]
    float velocidadAgua;
    [SerializeField]
    float fuerzaY;

    public bool estaPrendido;

    [SerializeField]
    GameObject chorroAguaPrefab;

    [SerializeField]
    Transform[] waterSources;

    void Start()
    {
        Prender();
    }


    IEnumerator ActivarDisparos()
    {
        yield return new WaitForSeconds(intervaloDisparos);
        if (estaPrendido)
        {

            DispararAgua();
        }
        else
        {
            StopCoroutine(ActivarDisparos());
        }
    }

        private void Prender()
    {
        //Debug.Log("PRENDIDO");
        estaPrendido = true;
        StartCoroutine(ActivarDisparos());
    }

    private void Apagar()
    {
        estaPrendido = false;
        //Debug.Log("APAGADO");

    }

    private void DispararAgua()
    {
        //Debug.Log("SPLASH");
        StartCoroutine(ActivarDisparos());

        
        var newAgua1 = Instantiate(chorroAguaPrefab, waterSources[2]);
        Vector3 direccion = this.transform.forward*velocidadAgua;
        direccion.y = fuerzaY;
        newAgua1.GetComponent<Rigidbody>().AddForce(direccion, ForceMode.Impulse);
        newAgua1.GetComponent<AguaAspersor>().damageValue = damageValue;

        direccion = this.transform.forward * -velocidadAgua;
        direccion.y = fuerzaY;
        var newAgua3 = Instantiate(chorroAguaPrefab, waterSources[3]);
        newAgua3.GetComponent<Rigidbody>().AddForce(direccion, ForceMode.Impulse);
        newAgua3.GetComponent<AguaAspersor>().damageValue = damageValue;

        direccion = this.transform.right * -velocidadAgua;
        direccion.y = fuerzaY;
        var newAgua4 = Instantiate(chorroAguaPrefab, waterSources[1]);
      newAgua4.GetComponent<Rigidbody>().AddForce(direccion, ForceMode.Impulse);
        newAgua4.GetComponent<AguaAspersor>().damageValue = damageValue;

        direccion = this.transform.right * velocidadAgua;
        direccion.y = fuerzaY;

        var newAgua2 = Instantiate(chorroAguaPrefab, waterSources[0]);
      newAgua2.GetComponent<Rigidbody>().AddForce(direccion, ForceMode.Impulse);
        newAgua2.GetComponent<AguaAspersor>().damageValue = damageValue;

    }
}
