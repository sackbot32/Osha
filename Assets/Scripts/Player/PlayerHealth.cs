using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,HealthInterface
{
    //Components
    [SerializeField]
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator anim;
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
    //Data
    private bool canTakeDamage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        canTakeDamage = true;
        currentHealth = maxHealth;
        LevelManager.instance.hud.UpdateHealth(currentHealth);
    }

    public void Heal(float ammount)
    {
        currentHealth += ammount;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        LevelManager.instance.hud.UpdateHealth(currentHealth);
    }

    public void TakeDamage(float damage, Vector2 damageSource)
    {
        if(canTakeDamage)
        {
            AudioManager.instance.PlaySfx(3, true);
            canTakeDamage = false;
            currentHealth -=damage;
            if(currentHealth <= 0 )
            {
                currentHealth = 0;
                Death();
            } else
            {
                Vector2 dir = new Vector2(transform.position.x, transform.position.y) - damageSource;
                rb.AddForce(dir.normalized * takeDamageKnockback);
            }
            LevelManager.instance.hud.UpdateHealth(currentHealth);
            StartCoroutine(AttackInvul());
        }
    }
    public void Death()
    {
        anim.SetBool("Dead", true);
        LevelManager.instance.EndGame(false);
    }

    public IEnumerator AttackInvul()
    {
        sprite.color = sprite.color - new Color(0,0,0,0.75f);
        anim.SetBool("Damaged", true);
        yield return new WaitForSeconds(invulDuration);
        anim.SetBool("Damaged", false);
        sprite.color = sprite.color + new Color(0, 0, 0, 0.75f);
        canTakeDamage = true;
    }


}
