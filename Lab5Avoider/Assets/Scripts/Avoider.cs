using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Avoider : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float avoidRange;

    [SerializeField]
    private float avoidSpeed;

    [SerializeField]
    private bool showGizmos;

    //private bool hadMadePoisson = false;

    //PoissonDiscSampler sampler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogWarning("NavMeshAgent missing. Add a NavMeshAgent component and bake a NavMesh");
        }
        agent.speed = avoidSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CreatePoissonDiskSampler();

        /*
        float size_x = 5f;
        float size_y = 5f;
        float radius = 2f;

        List<Vector3> candidates = new List<Vector3>();

        

        if (agent.remainingDistance <= 0)
        {
            if (!hadMadePoisson)
            {
                sampler = new PoissonDiscSampler(size_x, size_y, radius);
                hadMadePoisson = true;
            }
            
            foreach (var point in sampler.Samples())
            {
                Vector3 samplePosition = new Vector3(transform.position.x + point.x - size_x / 2f, transform.position.y, transform.position.z + point.y - size_y / 2f);

                if (CanPlayerSeePoint(samplePosition))
                {
                    if (showGizmos)
                    {
                        Debug.DrawLine(transform.position, samplePosition, Color.red);
                    }
                }
                else if (!CanPlayerSeePoint(samplePosition))
                {
                    if (showGizmos)
                    {
                        Debug.DrawLine(transform.position, samplePosition, Color.green);
                    }
                    candidates.Add(samplePosition);
                }
            }

            var clostestPoint = GetClosestPoint(candidates);
            if (Vector3.Distance(player.transform.position, transform.position) < avoidRange)
            {
                agent.destination = clostestPoint;
                hadMadePoisson = false;
                Debug.Log(clostestPoint);
            }

        }
        */
    }

    private bool CanPlayerSeePoint(Vector3 point)
    {
        if (Physics.Linecast(point, player.transform.position, out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private void CreatePoissonDiskSampler()
    {
        float size_x = 5f;
        float size_y = 5f;
        float radius = 2f;

        List<Vector3> candidates = new List<Vector3>();

        var sampler = new PoissonDiscSampler(size_x, size_y, radius);
        foreach (var point in sampler.Samples())
        {
            Vector3 samplePosition = new Vector3(transform.position.x + point.x - size_x/2f, transform.position.y, transform.position.z + point.y - size_y/2f);

            if (CanPlayerSeePoint(samplePosition))
            {
                if (showGizmos)
                {
                    Debug.DrawLine(transform.position, samplePosition, Color.red);
                }
            }
            else if (!CanPlayerSeePoint(samplePosition))
            {
                if (showGizmos)
                {
                    Debug.DrawLine(transform.position, samplePosition, Color.green);
                }
                candidates.Add(samplePosition);
            }
        }
        var clostestPoint = GetClosestPoint(candidates);
        if (Vector3.Distance(player.transform.position, transform.position) < avoidRange)
        {
            agent.destination = clostestPoint;
        }
    }
    
    private Vector3 GetClosestPoint(List<Vector3> points)
    {
        Vector3 closestPoint = Vector3.zero;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (var point in points)
        {
            Vector3 directionToTarget = point - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestPoint = point;
            }
        }

        return closestPoint;
    }
}
