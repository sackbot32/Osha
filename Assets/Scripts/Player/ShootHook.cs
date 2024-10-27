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
    [Tooltip("How much of the rope is changed")]
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
        mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(mousePos);
        }
        if (Input.GetMouseButton(1) && currentHook != null)
        {
            currentHook.GetComponent<Hook>().ChangeLength(-lengthAmountChange);
        }
    }
    /// <summary>
    /// Shoots the hook towards the objective that it recieves from the gameObject
    /// </summary>
    /// <param name="objective"></param>
    private void Shoot(Vector3 objective)
    {
        print(objective);
        if(currentHook != null)
        {
            currentHook.GetComponent<Hook>().SaveDestruction();
        }
        currentHook = Instantiate(hookPrefab,transform.position,Quaternion.identity);
        currentHook.transform.up = objective - currentHook.transform.position;
        currentHook.GetComponent<Rigidbody2D>().velocity = currentHook.transform.up * proyectileSpeed;


    }


}
