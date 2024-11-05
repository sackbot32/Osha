using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParalaxEffect : MonoBehaviour
{
    [SerializeField]
    private float paralaxXSpeed;
    [SerializeField]
    private float paralaxYSpeed;
    private Transform camPos;
    private Vector3 lastCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        camPos = GameObject.FindGameObjectWithTag("PlayerCam").transform;
        lastCameraPosition = camPos.position;
    }

    private void LateUpdate()
    {
        Vector3 backgroundMove = camPos.position - lastCameraPosition;
        transform.position += new Vector3(backgroundMove.x *paralaxXSpeed, backgroundMove.y* paralaxYSpeed,0);
        lastCameraPosition = camPos.position;
    }
}
