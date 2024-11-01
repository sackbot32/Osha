using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemyShooting : MonoBehaviour
{
    //Components
    private Transform aimPivot;
    private Transform shootPoint;
    [SerializeField] 
    private GameObject shotPrefab;
    public Transform target;
    //Setting
    [Header("Settings")]
    public float aimSpeed;
    public float timeBetweenShooting;
    [Header("Bullet Settings")]
    public float bulletSpeed;
    public float timeBeforeSelfDestruct;

    //Data
    private bool shooting;

    // Start is called before the first frame update
    void Start()
    {
        shooting = false;
        aimPivot = transform.GetChild(1);
        shootPoint = aimPivot.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            AimAtTarget();
            if(!shooting)
            {
                StartCoroutine(Shooting());
            }
        }
    }
    /// <summary>
    /// Aims at target by changing what the right of the pivot is
    /// </summary>
    private void AimAtTarget()
    {
        aimPivot.right = Vector3.Lerp(aimPivot.right, (target.position - transform.position).normalized, aimSpeed * Time.deltaTime);
        aimPivot.localEulerAngles = new Vector3(0, aimPivot.localEulerAngles.y, aimPivot.localEulerAngles.z);
    }
    /// <summary>
    /// Instances the shotPrefab every timeBetweenShooting
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shooting()
    {
        shooting = true;
        yield return new WaitForSeconds(timeBetweenShooting);
        while(target != null)
        {
            Instantiate(shotPrefab,shootPoint.position,Quaternion.identity).GetComponent<BulletMoving>().SetInfo(shootPoint.right,bulletSpeed,timeBeforeSelfDestruct);
            yield return new WaitForSeconds(timeBetweenShooting);
        }
        shooting = false;
    }
}
