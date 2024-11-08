using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBetweenPointsPhysics : MonoBehaviour, InteractInterface
{
    //Components
    private Rigidbody2D rb;
    //Settings
    public Vector2 whatToAddToPos;
    public float moveDuration;

    //Data
    private bool isOn;
    private Vector2 originalPos;
    private float timeObj;
    private float rate;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPos = transform.position;
    }

    private void Update()
    {
        //rate =  Vector2.Distance(originalPos, originalPos + whatToAddToPos) / moveDuration;
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
        Vector3 target = (originalPos + whatToAddToPos);
        while (isOn && timeObj < moveDuration)
        {
            rb.MovePosition(Vector2.Lerp(originalPos,target,timeObj/moveDuration));
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
            rb.MovePosition(target);
        }
    }

    private IEnumerator MoveBack()
    {
        isOn = false;
        Vector3 antiTarget = (originalPos + whatToAddToPos);
        while (!isOn && timeObj > 0)
        {
            rb.MovePosition(Vector2.Lerp(originalPos, antiTarget, timeObj / moveDuration));
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
