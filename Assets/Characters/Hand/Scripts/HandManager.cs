using System;
using UnityEngine;
using UnityEngine.AI;

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
    bool chasingPlayer = true;
    int phase = 1;

    // Start is called before the first frame update
    void Start()
    {
        HandReadyToAttack();
    }

    private void FixedUpdate()
    {
    }
    Vector3 target;
    public void DashTowardsPlayer()
    {
        //c·sin(B) / Sin(C)
        float C = Vector3.Angle(transform.position, centrePoint.position);
        C *= MathF.PI / 180;
        float B = MathF.PI - C * 2;
        float c = Vector3.Distance(transform.position, centrePoint.position);
        float distanceToPoint = c * Mathf.Sin(B) / Mathf.Sin(C);

        target = transform.forward * distanceToPoint;
        Debug.Log("transform.position = " + transform.position);
        Debug.Log("centrePoint.position = " + centrePoint.position);
        Debug.Log("C = " + C);
        Debug.Log("B = " + B);
        Debug.Log("c = " + c);
        Debug.Log("Distance to point = " + distanceToPoint);
        Debug.Log("Dash target = " + target);
        chasingPlayer = false;


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
        chasingPlayer = false;
        navMeshAgent.isStopped = true;
    }
    public void StartMoving()
    {
        chasingPlayer = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(RandomPoint(centrePoint.position, range));
    }

    Vector3 randomPoint;
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
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }
        Debug.Log(name + "Random position = " + hit.position);
        return Vector3.zero;
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
