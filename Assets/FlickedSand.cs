using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickedSand : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;

    bool moving = true;

    public IEnumerator MoveForward()
    {
        while(moving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<CrabHealth>().TakeDamage(damage);

            Remove();
        }
        if(other.CompareTag("Wall"))
        {
            Remove();
        }
    }

    public void Remove()
    {
        moving = false;
        Destroy(gameObject);
    }
}
