using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public float vel = 1f;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool SeMueve = false;
        if (Input.GetKey(KeyCode.W)) {
            SeMueve = true;
            transform.position += transform.forward * vel * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S)) {
            SeMueve = true;
            transform.position += transform.forward * (-vel) * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D)) {
            SeMueve = true;
            transform.position += transform.right * vel * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A)) {
            SeMueve = true;
            transform.position += transform.right * (-vel) * Time.deltaTime;
        }
        animator.SetBool("SeMueve", SeMueve);
    }
}
