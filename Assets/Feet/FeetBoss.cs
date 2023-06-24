using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FeetBoss : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    private Animator anim;
    //GameObject player;
    [Header("Movement")]
    public float range;
    public Transform centrePoint;

    [Header("Health")]
    public int maxHealth = 20;
    public int currentHealth;

    [Header("Attack")]
    public GameObject Blast;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();

        currentHealth = maxHealth;
        anim.SetInteger("Health", currentHealth);

        //player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

        
        //Freeze Rotations
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);

        //Random Movement
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                agent.SetDestination(point);
            }
        }
    }
    
    //Random Movement
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

    public void Damage()
    {
        currentHealth -= 1;
        anim.SetInteger("Health", currentHealth);
    }

    //Attacks
    public void Phase2()
    {
        if (anim.GetInteger("Health") <= 10)
        {
            int randomNumber = Random.Range(0, 2);
            if (randomNumber == 1)
            {
                anim.SetTrigger("Walk/RightStomp");
            }
            else
            {
                anim.SetTrigger("Walk/LeftStomp");
            }
        }
    }   
}