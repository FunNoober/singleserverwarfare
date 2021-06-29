using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class Waypoint : MonoBehaviour
{
    public GameObject[] nextWaypoints;

    public GameObject GetNextWaypoint()
    {
        GameObject nextWaypoint = nextWaypoints[Random.Range(0, nextWaypoints.Length)];
        if(nextWaypoint != null)
            return nextWaypoint;

        else
            return null;
    }

    void OnTriggerEnter(Collider other)
    {
        FriendlyAI fAI = other.GetComponent<FriendlyAI>();
        if(fAI)
        {  
            NavMeshAgent agent = fAI.GetComponent<NavMeshAgent>();
            agent.SetDestination(GetNextWaypoint().transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 5);
        foreach(GameObject gameObject in nextWaypoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, gameObject.transform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(gameObject.transform.position, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z));
        }
    }
}
