using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Amount of damage it does
    public float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //if it detects that it is an enemy it tries to tell if it has health, if it does, it makes it take damage
            if (collision.TryGetComponent(out HealthInterface healthInterface))
            {
                healthInterface.TakeDamage(damage, transform.position);
            }
        }
    }
}
