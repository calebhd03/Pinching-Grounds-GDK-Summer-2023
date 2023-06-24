using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrabCover : MonoBehaviour
{
    [SerializeField] GameObject modelToHide;

    float timeSinceBlocking = 0;
    bool blocking = false;

    void OnBlock(InputValue input)
    {
        float value = input.Get<float>();
        if(value == 0)
            blocking= false;
        else
            blocking= true;

        if (blocking)
        {
            modelToHide.SetActive(false);
            timeSinceBlocking = Time.time;

            Debug.Log("Blocking");
        }
        else if(!blocking)
        {
            modelToHide.SetActive(true);
            timeSinceBlocking = 0;

            Debug.Log("unBlocking");
        }
    }

    public bool GetBlocking()
    {
        return blocking;
    }

    public float timeBlocking()
    {
        return Time.time - timeSinceBlocking;
    }
}
