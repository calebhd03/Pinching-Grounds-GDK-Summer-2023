using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArc : MonoBehaviour
{
    public float speed = 10.0f;
    Vector3 startPosition;

    
    
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        speed = 10f;

        //Make Bullet Move
        transform.position += transform.forward * speed * Time.deltaTime;

        if ((transform.position - startPosition).magnitude > 20.0f)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
        if ((transform.position - startPosition).magnitude > 20.0f)
        {
            Destroy(gameObject);
        }
    }
}
