using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FriendlyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform waypointToMoveTo;
    public Waypoint[] waypointOnStart;

    public enum CurrentState
    {
        Patrolling,
        Chasing,
        Shooting
    };

    public CurrentState currentState;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypointOnStart[Random.Range(0, waypointOnStart.Length)].transform.position);
    }
}
