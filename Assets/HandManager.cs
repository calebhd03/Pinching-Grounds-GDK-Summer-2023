using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HandManager : MonoBehaviour
{
    
    [SerializeField] Animator handAnimator;
    [SerializeField] float parryTime;
    [SerializeField] int damage;
    [SerializeField] NavMeshAgent navMeshAgent;

    GameObject player;
    bool readyToAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        HandReadyToAttack();
    }

    private void FixedUpdate()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void SetPlayer(GameObject p)
    {
        player= p;
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
