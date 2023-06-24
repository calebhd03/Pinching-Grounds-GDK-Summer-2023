using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FeetBoss : MonoBehaviour
{
    NavMeshAgent agent;
    //GameObject player;
    public float range;
    public Transform centrePoint;

    Rigidbody RB;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        RB = GetComponent<Rigidbody>();
        //player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        RB.constraints = RigidbodyConstraints.FreezeRotation;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                agent.SetDestination(point);
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}