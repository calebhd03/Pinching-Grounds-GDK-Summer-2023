using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HandAttackManager : MonoBehaviour
{

    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float flickedSandSpeed;
    [SerializeField] float dashSpeed;

    [SerializeField] HandManager handManager;
    [SerializeField] MultipleVoiceline multipleVoiceline;
    [SerializeField] Animator handAnimator;
    [SerializeField] GameObject flickPrefab;
    [SerializeField] Transform flickPoint;
    [SerializeField] ParticleSystem handTrail;

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

        multipleVoiceline.Play();

        int attack = Random.Range(1, numberOfAttacks + 1);
        switch (attack)
        {
            case 1:
                handAnimator.SetTrigger("Flick");
                break;

            case 2:
                if(handManager.GetPhase() == 1)
                    handAnimator.SetTrigger("HandHandDrag");
                else
                    handAnimator.SetTrigger("HandHandDrag2");
             break;
        }

    }

    public void Flick()
    {
        GameObject flickedSand = Instantiate(flickPrefab, flickPoint.position, Quaternion.identity);
        Vector3 target = new Vector3(handManager.GetPlayerPosition().x, flickPoint.position.y, handManager.GetPlayerPosition().z);
        flickedSand.transform.LookAt(target);

        FlickedSand sandScript = flickedSand.GetComponent<FlickedSand>();
        sandScript.SetPhase(handManager.GetPhase());
        StartCoroutine(sandScript.MoveForward());
    }

    public void LookAtPlayer()
    {
        this.transform.LookAt(handManager.GetPlayerPosition());
    }

    public void HandDrag()
    {
        handTrail.Play();
        LookAtPlayer();
        handManager.DashTowardsPlayer();
        GetComponent<NavMeshAgent>().speed = dashSpeed;
    }

    public void StopHandDrag()
    {
        handTrail.Stop();
        handManager.StopDashTowardsPlayer();
        GetComponent<NavMeshAgent>().speed = navMeshSpeed;
    }
}
