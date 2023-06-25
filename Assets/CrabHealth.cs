using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabHealth : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] float invincibleTime;
    [SerializeField] float invincibilityDeltaTime;

    bool isInvincible = false;

    public void Start()
    { 
        SetToMaxHealth();
    }

    public bool TakeDamage(int damage)
    {
        Debug.Log("checking take damage: is Invincible = " + isInvincible);
        if(!isInvincible)
        {
            Debug.Log("Tool Damage");
            currentHealth -= damage;
            StartCoroutine(BecomeTemporarilyInvincible(invincibleTime));
            return true;
        }

        return false;
    }

    public void SetToMaxHealth()
    {
        currentHealth = maxHealth;
    }

    private IEnumerator BecomeTemporarilyInvincible(float timeInvincible)
    {
        isInvincible = true;
        for (float i = 0; i < timeInvincible; i += invincibilityDeltaTime)
        {

            // Alternate between 0 and 1 scale to simulate flashing
            if (model.transform.localScale == Vector3.one)
            {
                model.transform.localScale = Vector3.zero;
            }
            else
            {
                model.transform.localScale = Vector3.one;
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        model.transform.localScale = Vector3.one;
        isInvincible = false;
    }
}
