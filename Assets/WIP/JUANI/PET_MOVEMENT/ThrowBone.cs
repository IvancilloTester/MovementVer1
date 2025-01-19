using UnityEngine;

public class ThrowBone : MonoBehaviour
{
    private PlayerStats playerStats;
    public int HuesitosActuales;

    public Transform attackPoint;
    public GameObject TirarHuesito;

    public float throwCooldown = 1.0f;

    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwforce;
    public float throwUpwardForce;

    bool readyToThrow;

    /* El personaje puede tirar el hueso*/
    private void Start() {
        readyToThrow = true;
    }

    /* Llama a la funci�n de tirar los objetos*/
    private void Update() {
        ThrowObjects();
    }

    /* El hueso se va a lanzar desde donde est� mirando el personaje, ac� se crea el hueso y
       se tira. Tambi�n si se hace un tiro, se quita uno de los huesitos recolectados.
       Se puede volver a tirar otro huesito despu�s de un cooldown */
    private void Throw(int totalThrows) {
        readyToThrow = false; // Crear el huesito en la posici�n y rotaci�n del personaje
        GameObject Projectile = Instantiate(TirarHuesito, attackPoint.position, attackPoint.rotation); 
        Rigidbody ProjectileRB = Projectile.GetComponent<Rigidbody>(); // Calcular la fuerza a a�adir al huesito
        Vector3 forceToAdd = attackPoint.forward * throwforce + attackPoint.up * throwUpwardForce; 
        ProjectileRB.AddForce(forceToAdd, ForceMode.Impulse);
        Destroy(Projectile, 2f);
        GameManager.instance.playerStats.RemoveHuesitos(totalThrows); 
        Invoke("ResetThrow", throwCooldown);
    }

    /* Indica que el huesito se puede volver a tirar */
    private void ResetThrow() {
        readyToThrow = true ;
    }


    /* Si el personaje presiona el mouse, ya pas� el cooldown y hay huesitos disponibles,
       se puede tirar un hueso. En esta funci�n se encuentra del Script player stats la 
       cantidad de huesitos disponible */
    void ThrowObjects() {
        playerStats = Object.FindFirstObjectByType<PlayerStats>();
        if (playerStats != null) {
            HuesitosActuales = playerStats.huesitosActuales;
            //Debug.Log("huesitosActuales: " + HuesitosActuales);
            if (Input.GetKeyDown(throwKey) && readyToThrow && HuesitosActuales > 0) {
                Throw(1);
            }
        }
        else {
            Debug.LogError("No se encontr� PlayerStats");
        }
    }
}
