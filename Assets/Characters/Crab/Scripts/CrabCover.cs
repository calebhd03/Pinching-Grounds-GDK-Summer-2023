using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrabCover : MonoBehaviour
{
    [SerializeField] GameObject modelToHide;
    [SerializeField] AudioSource blockSound;
    [SerializeField] PlayerMovement playerMovement;

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
            Block();
        }
        else if(!blocking)
        {
            UnBlock();
        }
    }

    private void Block()
    {
        blockSound.Play();
        playerMovement.rb.velocity = Vector3.zero;
        modelToHide.SetActive(false);
        playerMovement.SetCanMove(false);

        timeSinceBlocking = Time.time;

        Debug.Log("Blocking");
    }

    private void UnBlock()
    {
        modelToHide.SetActive(true);
        playerMovement.SetCanMove(true);

        timeSinceBlocking = 0;

        Debug.Log("unBlocking");
    }

    public void PlayerDied()
    {
        Block();
    }

    public bool GetBlocking()
    {
        return blocking;
    }

    public float GetTimeBlocking()
    {
        return Time.time - timeSinceBlocking;
    }
}
