using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeAutoLine : MonoBehaviour
{
    private LineRenderer lRender;
    [Tooltip("added distance to rope")]
    public Vector3 addedPos;

    private void OnValidate()
    {
        lRender = GetComponent<LineRenderer>();
        lRender.SetPosition(0,transform.position);
        lRender.SetPosition(1,transform.position + addedPos);
    }


}
