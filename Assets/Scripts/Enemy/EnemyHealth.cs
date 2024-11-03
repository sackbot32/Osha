using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, HealthInterface
{
    //Components
    [SerializeField]
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    //Settings
    [Header("Settings")]
    [Tooltip("Max health")]
    public float maxHealth;
    [Tooltip("Current health")]
    public float currentHealth;
    [Tooltip("Duration of invulnerabilty")]
    public float invulDuration;
    [Tooltip("Force of knockback")]
    public float takeDamageKnockback;
    [Tooltip("Points given when dead")]
    public int points;
    //Data
    private bool canTakeDamage;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        canTakeDamage = true;
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage, Vector2 damageSource)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            currentHealth -= damage;
            AudioManager.instance.PlaySfx(9, true);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
            else
            {
                if(rb != null)
                {
                    Vector2 dir = new Vector2(transform.position.x, transform.position.y) - damageSource;
                    rb.AddForce(dir.normalized * takeDamageKnockback);
                }
            }
            StartCoroutine(AttackInvul());
        }
    }
    public void Heal(float ammount)
    {
        currentHealth += ammount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void Death()
    {
        LevelManager.instance.AddPoint(points);
        Destroy(gameObject);
    }

    

    
    public IEnumerator AttackInvul()
    {
        sprite.color = sprite.color - new Color(0, 0, 0, 0.75f);
        yield return new WaitForSeconds(invulDuration);
        sprite.color = sprite.color + new Color(0, 0, 0, 0.75f);
        canTakeDamage = true;
    }
}
