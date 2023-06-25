using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] float flickedSandSpeed;

    [SerializeField] HandManager handManager;
    [SerializeField] Animator handAnimator;
    [SerializeField] GameObject flickPrefab;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<HandManager>().PlayerHit(other.gameObject);
        }
    }


}
