using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public float damage;
    public bool destroySelf;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //if it detects that it is the player it tries to tell if it has health, if it does, it makes it take damage
            if (collision.TryGetComponent(out HealthInterface healthInterface))
            {
                healthInterface.TakeDamage(damage,transform.position);
                //if destroySelf is true, it destroys itself
                if(destroySelf)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
