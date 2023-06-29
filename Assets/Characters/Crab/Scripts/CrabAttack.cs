using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAttack : MonoBehaviour
{
    [SerializeField] Animator clawAnimator;
    [SerializeField] AudioSource attackSound;
    [SerializeField] float attackCoolDown;

    bool canAttack = true;

    public void OnAttack()
    {
        if(canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        attackSound.Play();
        clawAnimator.SetTrigger("Attack");
        StartCoroutine(AttackCoolDown());
    }

    IEnumerator AttackCoolDown()
    {
        float time = 0;
        canAttack= false;

        while(time < attackCoolDown)
        {
            yield return null;
            time += Time.deltaTime;
        }

        canAttack= true;
    }
}
