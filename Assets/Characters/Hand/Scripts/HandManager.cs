using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HandManager : MonoBehaviour
{

    [Header("Values")]
    [SerializeField] float parryTime;
    [SerializeField] float dashDistance;
    [SerializeField] int damage;
    [SerializeField] bool isLeftHand;

    [Header("Movement")]
    public float range;
    public Transform centrePoint;

    [Header("Refrences")]
    [SerializeField] Animator handAnimator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] HandAttack handAttack;
    [SerializeField] HandAttackManager handAttackManager;
    [SerializeField] GameObject player;

    bool readyToAttack = false;
    int phase = 1;
    Vector3 target;
    Vector3 randomPoint;

    // Start is called before the first frame update
    void Start()
    {
        HandReadyToAttack();
    }

    public void DashTowardsPlayer()
    {

        //only the wall layer
        int layer = 3;
        int layerMask = 1 << layer;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        target = hit.point;
        navMeshAgent.SetDestination(target);
        Debug.Log(name + "Target position = " + target);
    }

    public void StopDashTowardsPlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void SetPlayer(GameObject p)
    {
        player= p;
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }
    
    public void SetWhichHand(bool ifLeft)
    {
        isLeftHand= ifLeft;
    }

    public bool IsLeftHand()
    {
        return isLeftHand;
    }

    public void StopMoving()
    {
        navMeshAgent.isStopped = true;
    }
    public void StartMoving()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(RandomPoint(centrePoint.position, range));
    }

    //Random Movement
    Vector3 RandomPoint(Vector3 center, float range)
    {

        //flick hand goes to random point
        if (isLeftHand)
        {
            randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        }
        //dash hand goes to edge
        else
        {
            randomPoint = center + UnityEngine.Random.onUnitSphere * range;
        }
        randomPoint.y = 0;
        Debug.Log(name + "Random position = " + randomPoint);
        return randomPoint;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(randomPoint, .6f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(target, .6f);
    }
    public void HandWaitingHeight()
    {
        handAnimator.SetTrigger("HandWaiting");
        readyToAttack = false;
    }

    public void HandReadyToAttack()
    {
        handAnimator.SetTrigger("HandReady");
        readyToAttack = true;
    }

    public void Attack()
    {
        handAnimator.SetTrigger("HandAttack");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && readyToAttack)
        {
            Attack();
        }
    }

    public void PlayerHit(GameObject player)
    {
        CrabCover crabCover = player.GetComponent<CrabCover>();
        CrabHealth crabHealth = player.GetComponent<CrabHealth>();

        //check if player is blocking
        if (crabCover.GetBlocking())
        {
            //check if player parried
            if (parryTime >= crabCover.GetTimeBlocking())
            {
                Debug.Log("Hand stunned");
                handAnimator.SetTrigger("HandStunned");
            }
        }
        else
        {
            crabHealth.TakeDamage(damage);
        }
    }

    public void StartPhase2()
    {
        Debug.Log("PHASE 2!!!!!!!!!!!!!");
        phase = 2;
        handAttackManager.Phase2();
        handAnimator.SetTrigger("StartPhase2");
    }    

    public int GetPhase()
    {
        return phase;
    }

    public void BossDied()
    {
        handAnimator.SetTrigger("BossDied");
        StopMoving();
    }
}
