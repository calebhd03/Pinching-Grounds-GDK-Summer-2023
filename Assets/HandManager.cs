using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HandManager : MonoBehaviour
{
    
    [SerializeField] float parryTime;
    [SerializeField] int damage;
    [SerializeField] Animator handAnimator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] HandAttack handAttack;

    GameObject player;
    bool readyToAttack = false;
    bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        HandReadyToAttack();
    }

    private void FixedUpdate()
    {
        if(moving)
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
        moving= false;
        navMeshAgent.isStopped = true;
    }
    public void StartMoving()
    {
        moving = true;
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
}
