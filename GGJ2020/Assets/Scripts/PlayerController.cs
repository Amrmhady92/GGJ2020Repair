using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStat;
    Animator attackAnimator;
    private bool active = true;
    private AttackBehaviour attackBehaviour;
    private MovementController movementController;
    public bool invincible_ = false;
    public float invincibility_timer_ = 0.5f;
    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
            if (movementController != null) movementController.Active = active;
            else movementController = this.GetComponent<MovementController>();
            if (movementController != null) movementController.Active = active;
        }
    }

    private void Start()
    {
        if(playerStat == null)
        {
            Active = false;
            Debug.LogError("No PlayerStat on Player Controller Object");
        }
        attackBehaviour =  transform.GetChild(0).gameObject.GetComponent<AttackBehaviour>();
        movementController = this.GetComponent<MovementController>();
        if(movementController == null)
        {
            Active = false;
            Debug.LogError("No MovementController on Player Controller Object");
            return;
        }
        attackAnimator = this.GetComponentInChildren<Animator>();


    }
    private void Update()
    {
        if (!Active) return;

        if (Input.GetButtonDown("Fire_P"+playerStat.playerNumber))
        {
            if(attackBehaviour != null) attackBehaviour.Attack();
        }

        if (Input.GetButtonDown("Dash_P" + playerStat.playerNumber))
        {
            movementController.Dash();
        }
    }

    public void HealEffect()
    {
        playerStat.PlayerHP += GameManager.Instance.repairAmount;
    }

    public void TakeDamage(int damage_taken)
    {
        if (invincible_) return;

        playerStat.PlayerHP -= damage_taken;
        StopAllCoroutines();
        StartCoroutine(setInvincibility());
    }

    public IEnumerator setInvincibility()
    {
        invincible_ = true;
        yield return new WaitForSeconds(invincibility_timer_);
        invincible_ = false;
    }

    public void PlayAttackAnimation()
    {
        attackAnimator.SetTrigger("Attack");
    }
}
