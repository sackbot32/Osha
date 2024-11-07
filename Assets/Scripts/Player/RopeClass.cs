using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeClass : MonoBehaviour
{
    //Component
    private LineRenderer lineR;
    private DistanceJoint2D disJoint;
    //Setting
    [Header("Rope Settings")]
    [Tooltip("How fast will the line points will reach the desired point")]
    public float middlePointSpeed;
    [Tooltip("How much the middlePointSpeed is slowed down when tensed up")]
    public float tensedSpeedDivider;
    [Tooltip("How many middle points will the line have to render the rope")]
    public int howManyMiddlePoints;
    [Tooltip("the higher this is the less will the rope fall")]
    public float ropeTightnessMultiplier;
    [Tooltip("The lower it is the further you'll have to be for the rope to 'tense up' ")]
    [Range(0f, 1f)]
    public float tensionThreshold;
    [Tooltip("how wide will it be when tensed")]
    public float tensedWidth;
    [Tooltip("If this is turned on most value above will be automatically generated")]
    public bool automaticVisualRopeData;

    //Data
    private float distance;
        //this variable saves the old lenght of the distance joint, to detect change
    private float oldLength;
    void Start()
    {
        disJoint = GetComponent<DistanceJoint2D>();
        lineR = GetComponent<LineRenderer>();
        if(automaticVisualRopeData)
        {
            GenerateDataBasedOnDistance();
        } 
        lineR.positionCount = howManyMiddlePoints;
        
    }


    void Update()
    {
        if(disJoint.enabled && lineR.enabled)
        {
            if(automaticVisualRopeData && (oldLength != disJoint.distance))
            {
                GenerateDataBasedOnDistance();
                lineR.positionCount = howManyMiddlePoints;
            }
            if(disJoint.connectedBody != null)
            {
                RopeVisualMovement();
            }
            RopeVisualWidth();
        }
        
    }
    /// <summary>
    /// Function that recieves 2 vectors and creates a middle point dividing the distance by 2 and using the direction from a to b
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private Vector3 MiddlePoint(Vector3 a, Vector3 b)
    {
        Vector3 resultingMiddlePoint = Vector3.zero;
        float distance = Vector3.Distance(a, b);
        Vector3 direction = b - a;
        resultingMiddlePoint = a + (direction.normalized * (distance/2));
        return resultingMiddlePoint;
    }


    /// <summary>
    /// From the distance of the DistanceJoint2D all the properties of the visuals are created
    /// </summary>
    private void GenerateDataBasedOnDistance()
    {
        float lenght = disJoint.distance;
        howManyMiddlePoints = Mathf.CeilToInt(lenght * 6);
        tensedWidth = lenght / 2f;
        middlePointSpeed = lenght * 20;
        tensedSpeedDivider = middlePointSpeed / 10;
        oldLength = lenght;
    }

    /// <summary>
    /// Goes through the list of every point in the line renderer and puts it where it should be to simulate the movement of a rope
    /// For some reason needs to also be the one that gets the distance variable
    /// </summary>
    private void RopeVisualMovement()
    {
        lineR.SetPosition(0, transform.position);
        for (int i = 1; i < lineR.positionCount - 1; i++)
        {
            float downerForce = (lineR.positionCount - 1) / (float)i;
            if (ropeTightnessMultiplier != 0)
            {
                //For some reason I need create distance here or else it will seem invisible
                distance = Vector3.Distance(lineR.GetPosition(0), lineR.GetPosition(lineR.positionCount - 1));
                //print(distance);
                if (distance < (disJoint.distance - disJoint.distance * tensionThreshold))
                {
                    float tightness = ropeTightnessMultiplier * distance;
                    downerForce = downerForce / tightness;
                }
                else
                {
                    downerForce = 0;
                }
            }
            //print(downerForce);
            if (downerForce != 0)
            {
                //lineR.SetPosition(i, Vector3.Lerp(lineR.GetPosition(i), MiddlePoint(lineR.GetPosition(i - 1), lineR.GetPosition(i + 1) + Vector3.down * downerForce), middlePointSpeed * Time.deltaTime * (1 / downerForce)));
                lineR.SetPosition(i, Vector3.Lerp(lineR.GetPosition(i), MiddlePoint(lineR.GetPosition(i - 1), lineR.GetPosition(i + 1) + Vector3.down * downerForce), middlePointSpeed * Time.deltaTime));
            }
            else
            {
                //lineR.SetPosition(i, Vector3.Lerp(lineR.GetPosition(i), MiddlePoint(lineR.GetPosition(i - 1), lineR.GetPosition(i + 1)),10 * middlePointSpeed * Time.deltaTime));
                lineR.SetPosition(i, Vector3.Lerp(lineR.GetPosition(i), MiddlePoint(lineR.GetPosition(0), lineR.GetPosition(lineR.positionCount - 1)), (middlePointSpeed / tensedSpeedDivider) * Time.deltaTime));
            }
        }
        lineR.SetPosition(lineR.positionCount - 1, disJoint.connectedBody.position);
    }
    /// <summary>
    /// Detects when the player is far enough depending on tensionThreshold and simulates visual tension
    /// </summary>
    private void RopeVisualWidth()
    {
        if (distance < (disJoint.distance - disJoint.distance * tensionThreshold))
        {
            lineR.widthMultiplier = Mathf.Lerp(lineR.widthMultiplier, 1, Time.deltaTime * 2);
        }
        else
        {
            lineR.widthMultiplier = Mathf.Lerp(lineR.widthMultiplier, tensedWidth / distance, Time.deltaTime * 5);
        }
    }

   

    
}
