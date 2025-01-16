using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{

    private NavMeshAgent agent;

    [SerializeField]
    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
