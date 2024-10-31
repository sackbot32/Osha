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
    //Settings
    public float speed;
    public float jumpForce;
    //Data
    LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Detection(Vector2 origin, Vector2 direction,float length)
    {
        if (Physics2D.Raycast(origin, direction, length))
        {

        }
    }
}
