using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    //Components
    [SerializeField]
    public GameObject player;
    private LineRenderer playerLineR;
    private DistanceJoint2D playerDisJoint;
    private HingeJoint2D hinJoint;
    //Settings
    [Tooltip("what length will the rope have when it hits its target")]
    public float ropeDistance;

    private void Start()
    {
        playerLineR = player.GetComponent<LineRenderer>();
        playerDisJoint = player.GetComponent<DistanceJoint2D>();
        hinJoint = GetComponent<HingeJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GrabPoint"))
        {
            hinJoint.connectedBody = collision.GetComponent<Rigidbody2D>();
            hinJoint.enabled = true;
            playerDisJoint.connectedBody = GetComponent<Rigidbody2D>();
            playerDisJoint.distance = ropeDistance;
            playerDisJoint.enabled = true;
            playerLineR.enabled = true;
            transform.up = collision.transform.position - transform.position;
        }
    }
    /// <summary>
    /// Destroys the hook avoiding errors
    /// </summary>
    public void SaveDestruction()
    {
        playerDisJoint.enabled = false;
        playerLineR.enabled = false;
        Destroy(gameObject);
    }
    /// <summary>
    /// Recieves an ammount that the rope would gain or lose, multiplied by Time.deltaTime to not make it instant
    /// </summary>
    /// <param name="ammount"></param>
    public void ChangeLength(float ammount)
    {
        playerDisJoint.distance += ammount*Time.deltaTime;
    }
}
