using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    private DistanceEnemyShooting parent;
    private void Start()
    {
        parent = transform.parent.gameObject.GetComponent<DistanceEnemyShooting>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(7);
            parent.target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            parent.target = null;
        }
    }
}
