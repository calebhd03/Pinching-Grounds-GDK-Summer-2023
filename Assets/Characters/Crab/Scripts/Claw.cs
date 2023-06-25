using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            //other.GetComponentInParent<FeetBoss>().currentHealth-=1;
            FeetBoss feet = other.GetComponentInParent<FeetBoss>();
            if(feet != null)
                feet.Damage();
        }
        if (other.CompareTag("Left"))
        {
            //other.GetComponentInParent<FeetBoss>().currentHealth-=1;
            other.GetComponentInParent<FeetBoss>().DamageLeft();
        }
        if (other.CompareTag("Right"))
        {
            //other.GetComponentInParent<FeetBoss>().currentHealth-=1;
            other.GetComponentInParent<FeetBoss>().DamageRight();
        }
    }
}
