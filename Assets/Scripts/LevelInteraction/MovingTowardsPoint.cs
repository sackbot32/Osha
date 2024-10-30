using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTowardsPoint : MonoBehaviour, InteractInterface
{
    //Settings
    public Transform objPoint;
    public float moveDuration;

    //Data
    private bool isOn;
    private Vector2 originalPos;
    private float timeObj;
    private float rate;

    private void Start()
    {

        originalPos = transform.position;
        timeObj = 0;
    }

    private void Update()
    {
        // rate =  Vector2.Distance(originalPos, originalPos + whatToAddToPos) / moveDuration;
    }

    public bool GetIsOn()
    {
        return isOn;
    }

    public void TurnOff()
    {
        StartCoroutine(MoveBack());
    }

    public void TurnOn()
    {
        StartCoroutine(MoveTowards());
    }

    private IEnumerator MoveTowards()
    {
        isOn = true;
        while (timeObj < moveDuration && isOn)
        {
            transform.position = Vector2.Lerp(originalPos, objPoint.position, timeObj / moveDuration);
            timeObj += Time.deltaTime;
            if (timeObj > moveDuration)
            {
                break;
            }
            yield return null;
        }
        if (isOn)
        {
            timeObj = moveDuration;
            transform.position = objPoint.position;
        }
    }

    private IEnumerator MoveBack()
    {
        isOn = false;
        while (timeObj > 0 && !isOn)
        {

            transform.position = Vector2.Lerp(originalPos, objPoint.position, timeObj / moveDuration);
            timeObj -= Time.deltaTime;
            if (timeObj < 0)
            {
                break;
            }
            yield return null;
        }
        if (!isOn)
        {
            timeObj = 0;
            transform.position = originalPos;
        }
    }
}
