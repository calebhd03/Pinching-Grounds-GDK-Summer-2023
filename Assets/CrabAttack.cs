using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAttack : MonoBehaviour
{
    [SerializeField] Animator clawAnimator;
    [SerializeField] float attackCoolDown;

    bool canAttack = true;

    void OnAttack()
    {
        if(canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
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
