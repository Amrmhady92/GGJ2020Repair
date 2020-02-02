using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashingAttack : AttackBehaviour
{
    public Collider2D attack_box_;
    
    public float attack_duration_;
    public bool attacked_ = false;
    private PlayerController myController;
    private void Start()
    {
        attack_box_ = GetComponent<Collider2D>();
        attack_box_.enabled = false;
        myController = this.GetComponentInParent<PlayerController>();
    }

    public override void Attack()
    {
        if (!attacked_)
        {
            StopAllCoroutines();
            StartCoroutine(ActivateAttack());
            if (myController != null) myController.PlayAttackAnimation();
        }
    }

    private IEnumerator ActivateAttack()
    {
        FindObjectOfType<AudioManager>().Play("Attack" + myController.playerStat.playerNumber);
        attacked_ = true;
        attack_box_.enabled = true;

        yield return new WaitForSeconds(attack_duration_);
        attack_box_.enabled = false;

        
        yield return new WaitForSeconds(attack_cooldown_);
        attacked_ = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null && collision.transform.parent != this.transform.parent)
        {
            playerCont.TakeDamage(attackDamage);
            FindObjectOfType<AudioManager>().Play("Hit" + playerCont.playerStat.playerNumber);
            if (playerCont.playerStat.isDead)
            {
                FindObjectOfType<AudioManager>().Play("Death" + playerCont.playerStat.playerNumber);
            }
        }
    }
}

