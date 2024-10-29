using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    //Components
    private Rigidbody2D rb;
    [SerializeField]
    private InputActionReference directionInput;
    [SerializeField]
    private InputActionReference jumpInput;
    private Transform feetTransform;
    [Header("Move Settings")]
    //Movement Settings
    [Tooltip("Player's walking speed")]
    public float walkingSpeed;
    [Tooltip("Player's max walking speed")]
    public float maxSpeed;
    [Tooltip("By what is the speed divided by when the player stops")]
    public float brakingAmmount;
    [Header("Jump Settings")]
    [Tooltip("How much force it adds when jumping")]
    public float jumpForce;
    [Tooltip("How much gravity is changed after jumping")]
    public float gravityMultAfterJump;
    [Tooltip("How long is the beam to detect the ground")]
    public float jumpDetectLength;
    //Data
    private Vector2 direction;
    public bool canJump;
    private LayerMask layerMask;
    void Start()
    {
        //Get the components
        rb = GetComponent<Rigidbody2D>();
        //Initialize variables when needed
        direction = Vector2.zero;
        //Get the feet, which are the second child of the player
        feetTransform = transform.GetChild(1);
        //Get the correct mask to put in a variable
        layerMask = LayerMask.GetMask("Ground");
    }


    void Update()
    {
        direction = directionInput.action.ReadValue<Vector2>();
        
        DetectGround();
        if (jumpInput.action.IsPressed())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move(direction);
    }

    /// <summary>
    /// Recieves a Vector2 and moves character in the X axis
    /// </summary>
    /// <param name="dir"></param>
    private void Move(Vector2 dir)
    {
        //When the absolute value of the value of velocity.x is bellow the maxSpeed, then it adds force in that direction
        if(Mathf.Abs(rb.velocity.x) <= maxSpeed)
        {
            if(rb.velocity.x < 0 && dir.x > 0)
            {
                rb.velocity = new Vector2(1, rb.velocity.y);
            }
            if(rb.velocity.x > 0 && dir.x < 0)
            {
                rb.velocity = new Vector2(-1, rb.velocity.y);
            }
            rb.AddForce(dir.x * Vector2.right * walkingSpeed);
        } else
        {
            //If the speed goes over maxSpeed then it gets capped out
            if(rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            } else
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
        }
        //When the value of direction.x is 0, the velocity will start to be divided by brakingAmmount
        if (dir.x == 0 && brakingAmmount != 0 && canJump) 
        {
            rb.velocity = new Vector2(rb.velocity.x / brakingAmmount, rb.velocity.y);
            if(Mathf.Abs(rb.velocity.x) < 0.05f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    /// <summary>
    /// Adds force upwards with the force indicated by the jumpForce variable
    /// </summary>
    private void Jump()
    {
        if(canJump)
        {
            canJump = false;
            //rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime * gravityMultAfterJump);
            rb.gravityScale = gravityMultAfterJump;
        }
    }
    /// <summary>
    /// Shoots a ray downwards to detect if we are hitting an item with the LayerMask Ground
    /// </summary>
    private void DetectGround()
    {
        Debug.DrawRay(feetTransform.position, Vector3.down*jumpDetectLength, Color.red);
        if (Physics2D.Raycast(feetTransform.position,Vector3.down, jumpDetectLength,layerMask))
        {
            canJump = true;
            rb.gravityScale = 1;
        }
    }
}
