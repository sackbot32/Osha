using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootHook : MonoBehaviour
{
    //Components
    [SerializeField]
    private GameObject hookPrefab;
    //Settings
    [Header("Settings")]
    [Tooltip("The speed at which the proyectile travels")]
    public float proyectileSpeed;
    [Tooltip("How far the proyecitle travels")]
    public float proyectileDistanceTarget;
    [Tooltip("How long is the rope when the hook gets to the target, idealy is the same as proyectileDistanceTarget")]
    public float lengthRope;
    [Tooltip("How much of the rope is changed when changing its length")]
    public float lengthAmountChange;
    //Data
    private GameObject currentHook;
    private Vector3 mousePos;


    private void Start()
    {
        hookPrefab.GetComponent<Hook>().player = gameObject;
    }

    void Update()
    {
        mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(mousePos);
        }
        if (Input.GetMouseButton(1) && currentHook != null)
        {
            currentHook.GetComponent<Hook>().ChangeLength(-lengthAmountChange);
        }
        if(Input.GetMouseButtonDown(2) && currentHook != null)
        {
            currentHook.GetComponent<Hook>().SafeDestruction();
        }
    }
    /// <summary>
    /// Shoots the hook towards the objective that it recieves from the gameObject
    /// </summary>
    /// <param name="objective"></param>
    private void Shoot(Vector3 objective)
    {
        if (currentHook != null)
        {
            currentHook.GetComponent<Hook>().SafeDestruction();
        }
        currentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity);
        currentHook.GetComponent<Hook>().ropeDistance = lengthRope;
        StartCoroutine(DestroyProyectileOnTime());
        currentHook.transform.up = (objective - currentHook.transform.position).normalized;
        currentHook.GetComponent<Rigidbody2D>().velocity = currentHook.transform.up * proyectileSpeed;


    }
    /// <summary>
    /// Using the target distance divided by the speed we will destroy the proyectile on the seconds given by that
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyProyectileOnTime()
    {
        if(currentHook != null)
        {
            Vector3 firstPoint = currentHook.transform.position;
            float timeToDestroy = proyectileDistanceTarget/proyectileSpeed;
            yield return new WaitForSeconds(timeToDestroy);
            float debugDistance = Vector2.Distance(firstPoint, currentHook.transform.position);
            print("distancia: " + debugDistance);
            if(!currentHook.GetComponent<HingeJoint2D>().enabled) 
            {
                currentHook.GetComponent<Hook>().SafeDestruction();
            }
        } else
        {

            yield return null;
        }
    }


}
