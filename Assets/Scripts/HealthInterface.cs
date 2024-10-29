using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HealthInterface 
{

    public void TakeDamage(float damage, Vector2 damageSource);

    public void Heal(float ammount);

    public void Death();

    public IEnumerator AttackInvul();

}
