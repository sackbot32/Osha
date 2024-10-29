using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    public float damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.TryGetComponent(out HealthInterface healthInterface))
            {
                healthInterface.TakeDamage(damage,transform.position);
            }
        }
    }
}
