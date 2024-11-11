using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [Header("Settings")]
    public float damage;
    public bool destroySelf;
    [Header("Teleport after damage")]
    public bool teleportPlayer;
    public Transform teleportPoint;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //if it detects that it is the player it tries to tell if it has health, if it does, it makes it take damage
            if (collision.TryGetComponent(out HealthInterface healthInterface))
            {
                if (!teleportPlayer) 
                { 
                    healthInterface.TakeDamage(damage,transform.position);
                } else
                {
                    healthInterface.TakeDamage(damage, Vector2.zero);
                    if (collision.GetComponent<Rigidbody2D>())
                    {
                        print("para");
                        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        collision.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    }
                    collision.transform.position = teleportPoint.position;
                }

                //if destroySelf is true, it destroys itself
                if(destroySelf)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
