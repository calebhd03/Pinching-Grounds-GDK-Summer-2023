using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] Rigidbody rigidbody;
    [SerializeField] PlayerSettings playerSettings;
    [SerializeField] float movementSpeed;
    [SerializeField] Joystick joystick;

    public Rigidbody rb;
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    Vector3 moveVelocity;
    Vector3 movement;
    Vector3 look;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = movement * movementSpeed;
            LookAtMouse();
        }
    }

    private void OnMove(InputValue input)
    {
        Vector2 movementInput = input.Get<Vector2>();
        movement = new Vector3(movementInput.x, 0, movementInput.y);
        moveVelocity = movement * movementSpeed;
    }
    

    private void OnLook(InputValue input)
    {
        Vector2 lookInput = input.Get<Vector2>();
        look = new Vector3(0, lookInput.x, 0);
    }

    public void ChangeToUI()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        input.actions.FindActionMap("Player").Disable();
        input.actions.FindActionMap("UI").Enable();
    }

    ///Points gun at mouse world position
    public void LookAtMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
