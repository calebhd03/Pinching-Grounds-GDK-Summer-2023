using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HandAttackManager : MonoBehaviour
{

    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float flickedSandSpeed;

    [SerializeField] HandManager handManager;
    [SerializeField] Animator handAnimator;
    [SerializeField] GameObject flickPrefab;
    [SerializeField] Transform flickPoint;

    int numberOfAttacks = 2;
    bool canAttack = false;
    float navMeshSpeed;


    private void Start()
    {
        navMeshSpeed = GetComponent<NavMeshAgent>().speed;
    }
    IEnumerator wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReadyToAttack();
    }

    public void ReadyToAttack()
    {
        canAttack = true;
        int attack = Random.Range(1, numberOfAttacks + 1);

        switch (attack)
        {
            case 1:
                handAnimator.SetTrigger("Flick");
                break;
            case 2:
                handAnimator.SetTrigger("HandHandDrag");

                break;
        }

    }

    float ClipLength()
    {
        return handAnimator.GetCurrentAnimatorStateInfo(1).length;
    }

    public void Flick()
    {
        GameObject flickedSand = Instantiate(flickPrefab, flickPoint.position, Quaternion.identity);
        flickedSand.transform.LookAt(handManager.GetPlayerPosition());

        StartCoroutine(flickedSand.GetComponent<FlickedSand>().MoveForward());
    }

    public void LookAtPlayer()
    {
        this.transform.LookAt(handManager.GetPlayerPosition());
    }

    public void HandDrag()
    {
        handManager.DashTowardsPlayer();
        GetComponent<NavMeshAgent>().speed = 6;
    }

    public void StopHandDrag()
    {
        handManager.StopDashTowardsPlayer();
        GetComponent<NavMeshAgent>().speed = navMeshSpeed;
    }
}
