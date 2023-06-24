using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int currentHealth;

    public void Start()
    { 
        SetToMaxHealth();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void SetToMaxHealth()
    {
        currentHealth = maxHealth;
    }
}
