using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    //Component
    private Rigidbody2D rb;
    private Transform forwardDetect;
    private Transform groundDetect;
    private Transform jumpDetect;
    private Animator anim;
    private SpriteRenderer sRender;
    //Settings
    [Header("Settings")]
    [Tooltip("How fast it goes from side to side")]
    public float speed;
    [Tooltip("With what force it jumps")]
    public float jumpForce;
    [Tooltip("The lenght of the ray that detect walls")]
    public float detectWallLength;
    [Tooltip("The lenght of the ray that detect the ground")]
    public float detectGroundLength;
    //Data
    private LayerMask layer;
    private bool right;

    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        forwardDetect = transform.GetChild(2).transform;
        groundDetect = transform.GetChild(3).transform;
        jumpDetect = transform.GetChild(4).transform;
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        sRender = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detect wall
        if (Detection(forwardDetect.position,(forwardDetect.right * rb.velocity.x).normalized,detectWallLength))
        {
            //If when detecting wall it can tell it can jump it then it does it
            if(!Detection(jumpDetect.position, (jumpDetect.right * rb.velocity.x).normalized, detectWallLength))
            {
                anim.SetBool("Falling", true);
                Jump();
            } else
            {
                //If it not its just turns around
                ChangeDetectPos();
            }
        }
        //Detect lacking ground
        if (!Detection(groundDetect.position, -transform.up, detectGroundLength/ 2))
        {
            if(!Detection(groundDetect.position, -transform.up, detectGroundLength * 4))
            {

                ChangeDetectPos();
            }
        } else
        {
            anim.SetBool("Falling", false);
        }
        Move();
    }
    /// <summary>
    /// Sends a raycast from a given origin point towards a given direction with the length give
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private bool Detection(Vector2 origin, Vector2 direction,float length)
    {
        bool hasDetected = false;
        Debug.DrawRay(origin,direction*length,Color.yellow,1f);
        if (Physics2D.Raycast(origin, direction, length,layer))
        {
            hasDetected = true;
        }
        return hasDetected;
    }
    /// <summary>
    /// Changes the point from where the detectors are when called while at the same time setting right to the opossite
    /// </summary>
    private void ChangeDetectPos()
    {
        right = !right;
        sRender.flipX = !right;
        forwardDetect.localPosition = new Vector2(-forwardDetect.localPosition.x, forwardDetect.localPosition.y);
        groundDetect.localPosition = new Vector2(-groundDetect.localPosition.x, groundDetect.localPosition.y);
        jumpDetect.localPosition = new Vector2(-jumpDetect.localPosition.x, jumpDetect.localPosition.y);
    }
    /// <summary>
    /// moves the character in the x axis with the given speed with the direction gotten from the "right" variable
    /// </summary>
    private void Move()
    {
        rb.velocity = new Vector2(right ? -speed : speed, rb.velocity.y);

    }
    /// <summary>
    /// Jumps with a given force
    /// </summary>
    private void Jump()
    {
        rb.AddForce(transform.up *jumpForce);

    }

}
