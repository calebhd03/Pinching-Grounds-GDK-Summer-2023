using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HandManager : MonoBehaviour
{
    
    [SerializeField] float parryTime;
    [SerializeField] float dashDistance;
    [SerializeField] int damage;
    [SerializeField] Animator handAnimator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] HandAttack handAttack;
    [SerializeField] GameObject player;

    bool readyToAttack = false;
    bool chasingPlayer = true;
    int phase = 2;

    // Start is called before the first frame update
    void Start()
    {
        HandReadyToAttack();
    }

    private void FixedUpdate()
    {
        if(chasingPlayer)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    public void DashTowardsPlayer()
    {
        chasingPlayer= false;

        Ray targetLine = new Ray(transform.position,  player.transform.position - transform.position);
        Vector3 target;

        
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.blue, 1f);

        RaycastHit hit; LayerMask layerMask = 3;
        if(Physics.Raycast(targetLine, out hit, layerMask))
        {
            target = hit.point;
        }
        else
        {
            target = targetLine.GetPoint(dashDistance);
        }


        navMeshAgent.SetDestination(target);
    }
    public void StopDashTowardsPlayer()
    {
        chasingPlayer = true;
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

    public void StopMoving()
    {
        chasingPlayer = false;
        navMeshAgent.isStopped = true;
    }
    public void StartMoving()
    {
        chasingPlayer = true;
        navMeshAgent.isStopped = false;
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
        phase = 2;
        handAnimator.SetTrigger("StartPhase2");
    }    

    public int GetPhase()
    {
        return phase;
    }

    public void BossDied()
    {

    }
}