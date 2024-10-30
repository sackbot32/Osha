using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HealthInterface 
{
    /// <summary>
    /// Reduces health based on damage given while also giving where the damage is coming from
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="damageSource"></param>
    public void TakeDamage(float damage, Vector2 damageSource);
    /// <summary>
    /// Heals health by an ammount given
    /// </summary>
    /// <param name="ammount"></param>
    public void Heal(float ammount);
    /// <summary>
    /// Called on 0 or less health
    /// </summary>
    public void Death();
    /// <summary>
    /// Function that makes the character invulnerable for a time
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttackInvul();

}
