using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Avoider : MonoBehaviour
{
    private NavMeshAgent agent;

    public GameObject player;

    public float avoidRange;

    public float avoidSpeed;

    public bool showGizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
