using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private float speed = 10;
    private Vector3 move;


    public float gravity = -40f;
    public float jumpHeight = 2.2f;
    private Vector3 velocity;


    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGrounded;

    public Animator animator;

    InputAction movement;
    InputAction jump;

    // Start is called before the first frame update
    void Start()
    {
        jump = new InputAction("Jump", binding: "<keyboard>/space");
        movement = new InputAction("Movement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<keyboard>/w")
            .With("Down", "<keyboard>/s")
            .With("Left", "<keyboard>/a")
            .With("Right", "<keyboard>/d");

        movement.Enable();
        jump.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        float x = movement.ReadValue<Vector2>().x;
        float z = movement.ReadValue<Vector2>().y;

        animator.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);


        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        if (isGrounded)
        {
            if (Mathf.Approximately(jump.ReadValue<float>(),1))
            {
                Jump();
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }
}
