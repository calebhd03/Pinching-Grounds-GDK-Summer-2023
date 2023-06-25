using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickedSand : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] float timeBetweenBreak;
    [SerializeField] GameObject sandPrefab;


    int phase = 1;
    int splitsSpawned = 0;
    bool moving = true;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }

    public IEnumerator MoveForward()
    {
        if(phase ==2)
        {
            StartCoroutine(Split());
        }
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

        while(moving)
        {
            if (this == null)
                break;

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

    private void OnDestroy()
    {
        this.moving = false;
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

        Vector3 tilted = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 30, transform.eulerAngles.z);
        SpawnSand(tilted);
        Vector3 opTilted = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 30, transform.eulerAngles.z);
        SpawnSand(opTilted);

        splitsSpawned++;

        if(splitsSpawned <2)
        {
            StartCoroutine(Split());
        }
    }

    void SpawnSand(Vector3 offset)
    {
        GameObject flickedSand = Instantiate(sandPrefab);
        flickedSand.transform.LookAt(transform.forward);

        flickedSand.transform.eulerAngles = offset;


        FlickedSand sandScript = flickedSand.GetComponent<FlickedSand>();
        sandScript.SetPhase(1);
        StartCoroutine(sandScript.MoveForward());
    }

}
