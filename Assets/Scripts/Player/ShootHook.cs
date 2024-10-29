using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootHook : MonoBehaviour
{
    //Components
    [SerializeField]
    private GameObject hookPrefab;
    [SerializeField]
    private InputActionReference shootHookInput;
    [SerializeField]
    private InputActionReference mousePositionInput;
    [SerializeField]
    private InputActionReference rightJoyStickDirInput;
    [SerializeField]
    private InputActionReference shortenRopeInput;
    [SerializeField]
    private InputActionReference cutRopeInput;
    [SerializeField]
    private InputActionReference quickCutRopeInput;
    [SerializeField]
    private PlayerInput playerInput;
    //Settings
    [Header("Settings")]
    [Tooltip("The speed at which the proyectile travels")]
    public float proyectileSpeed;
    [Tooltip("How far the proyecitle travels")]
    public float proyectileDistanceTarget;
    [Tooltip("How long is the rope when the hook gets to the target, idealy is the same as proyectileDistanceTarget")]
    public float lengthRope;
    [Header("Shortening Settings")]
    [Tooltip("How much of the rope is changed when changing its length")]
    public float lengthAmountChange;
    [Tooltip("How much of the rope is changed when changing its length on quick shortening")]
    public float quickLengthAmountChange;
    [Tooltip("When quick shortening how fast does it do it")]
    public float quickShorteningRate;
    [Tooltip("What at what length does the quickShortening ends")]
    public float shorteningFinalLength;
    //Data
    private GameObject currentHook;
    private Vector3 mousePos;
    private Vector2 dir;
    private bool shortening;


    private void Start()
    {
        shortening = false;
        hookPrefab.GetComponent<Hook>().player = gameObject;
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (playerInput.currentControlScheme.Equals(InputConstants.KEYBOARDMOUSESCHEME))
        {
            mousePos = new Vector3(Camera.main.ScreenToWorldPoint(mousePositionInput.action.ReadValue<Vector2>()).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            if (shootHookInput.action.WasPressedThisFrame())
            {
                Shoot(mousePos);
            }
        }
        if (playerInput.currentControlScheme.Equals(InputConstants.CONTROLLERSCHEME))
        {
            dir = rightJoyStickDirInput.action.ReadValue<Vector2>();
            if(dir != Vector2.zero)
            {

                if (shootHookInput.action.WasPressedThisFrame())
                {
                    Shoot(dir);
                }
            }
        }
        if (shortenRopeInput.action.IsPressed() && currentHook != null)
        {
            if(currentHook != null && GetComponent<DistanceJoint2D>().distance > shorteningFinalLength)
            {
                currentHook.GetComponent<Hook>().ChangeLength(-lengthAmountChange);
            }
        }
        if (quickCutRopeInput.action.WasPerformedThisFrame() && currentHook != null && !shortening)
        {
            StartCoroutine(QuickShorten());
        }

        if(cutRopeInput.action.WasPressedThisFrame() && currentHook != null)
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
    /// Shoots the hook in the direction it recives
    /// </summary>
    /// <param name="objective"></param>
    private void Shoot(Vector2 dir)
    {
        if (currentHook != null)
        {
            currentHook.GetComponent<Hook>().SafeDestruction();
        }
        currentHook = Instantiate(hookPrefab, transform.position, Quaternion.identity);
        currentHook.GetComponent<Hook>().ropeDistance = lengthRope;
        StartCoroutine(DestroyProyectileOnTime());
        currentHook.transform.up = dir.normalized;
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
            if (currentHook != null)
            {
                float debugDistance = Vector2.Distance(firstPoint, currentHook.transform.position);
                //print("distancia: " + debugDistance);
                if(!currentHook.GetComponent<HingeJoint2D>().enabled) 
                {
                    currentHook.GetComponent<Hook>().SafeDestruction();
                }
            }
        } else
        {

            yield return null;
        }
    }
    /// <summary>
    /// Shortens the rope quickly and destroys the hook
    /// </summary>
    private IEnumerator QuickShorten()
    {
        shortening = true;
        while(currentHook != null && GetComponent<DistanceJoint2D>().distance > shorteningFinalLength)
        {
            print(GetComponent<DistanceJoint2D>().distance);
            currentHook.GetComponent<Hook>().ChangeLength(-quickLengthAmountChange);
            yield return new WaitForSeconds(quickShorteningRate);
        }
        if (currentHook != null)
        {
            currentHook.GetComponent<Hook>().SafeDestruction();
        }
        shortening = false;
    }


}
