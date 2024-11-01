using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    //Component
    private Rigidbody2D rb;
    //Setting
    public Vector2 dir;
    public float speed;
    public float timeBeforeSelfDestruct;
    //Data
    private Coroutine currentCo;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
        if(timeBeforeSelfDestruct != 0)
        {
            currentCo = StartCoroutine(SelfDestruct());
        }
    }
    /// <summary>
    /// Destroys itself after timeBeforeSelfDestruct seconds
    /// </summary>
    /// <returns></returns>
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeBeforeSelfDestruct);
        Destroy(gameObject);
    }
    /// <summary>
    /// Recieves Data to set the proyectile
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="speed"></param>
    /// <param name="timeBeforeDestroy"></param>
    public void SetInfo(Vector2 newDir, float newSpeed, float newTimeBeforeDestroy)
    {
        if(currentCo != null)
        {
            StopCoroutine(currentCo);
        }
        dir = newDir;
        transform.right = newDir;
        speed = newSpeed;
        timeBeforeSelfDestruct = newTimeBeforeDestroy;
        if (timeBeforeSelfDestruct != 0)
        {
            currentCo = StartCoroutine(SelfDestruct());
        }

    }

}
