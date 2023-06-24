using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movement * movementSpeed * Time.deltaTime;
    }

    private void OnMove(InputValue input)
    {
        Vector2 movementInput = input.Get<Vector2>();
        movement = new Vector3(movementInput.x, 0, movementInput.y);
    }
}
