using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickedSand : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] float timeBetweenBreak;


    int phase = 1;
    bool moving = true;

    public IEnumerator MoveForward()
    {
        while(moving)
        {
            if(phase ==2)
            {
                StartCoroutine(Split());
            }

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

    public void SetPhase(int phase)
    {
        this.phase = phase;
    }

    IEnumerator Split()
    {
        float time = 0;
        while(time < timeBetweenBreak)
        {
            yield return null;
            time+= Time.deltaTime;
        }

        GameObject flickedSand = Instantiate(gameObject); 
        flickedSand.transform.LookAt(transform.forward);

        Vector3 tilted = Quaternion.Euler(0, 30, 0) * Vector3.up;
        flickedSand.transform.Rotate(tilted);


        FlickedSand sandScript = flickedSand.GetComponent<FlickedSand>();
        sandScript.SetPhase(1);
        StartCoroutine(sandScript.MoveForward());
    }

}
