using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackActivation : MonoBehaviour
{
    //Components
    [SerializeField]
    private Collider2D attackCollider;
    [SerializeField]
    private SpriteRenderer[] hammerPose;
    [SerializeField]
    private InputActionReference attackInput;
    //Settings
    [Header("Settings")]
    [Tooltip("The time the attack exist for")]
    public float timeAttacking;
    [Tooltip("The time till next attack")]
    public float attackCooldown;
    //Data
    private bool attacking;
    private void Start()
    {
        attacking = false;
        attackCollider.enabled = false;
        hammerPose[0].enabled = true;
        hammerPose[1].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackInput.action.WasPressedThisFrame() && !attacking && Time.timeScale == 1)
        {
            StartCoroutine(Attacking());
        }
    }

    /// <summary>
    /// Courritine that activates the attack hitbox for a time
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attacking()
    {
        attacking = true;
        hammerPose[0].enabled = false;
        hammerPose[1].enabled = true;
        AudioManager.instance.PlaySfx(4, true);

        attackCollider.enabled = true;
        yield return new WaitForSeconds(timeAttacking);
        hammerPose[0].enabled = true;
        hammerPose[1].enabled = false;
        attackCollider.enabled = false;
        StartCoroutine(CoolDown());

    }
    /// <summary>
    /// Courritine that doesnt allow to attack for a time
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }
}
