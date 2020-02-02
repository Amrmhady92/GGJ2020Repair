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
        attacked_ = true;
        attack_box_.enabled = true;

        //NEW
        //var hits = Physics2D.CircleCastAll(this.transform.position, attackRadius, this.transform.up);
        //PlayerController playerCont;
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    playerCont = hits[i].transform.GetComponent<PlayerController>();
        //    if (playerCont != null && hits[i].transform != this.transform.parent)
        //    {
        //        playerCont.TakeDamage(attackDamage);
        //    }
        //}

        yield return new WaitForSeconds(attack_duration_);
        attack_box_.enabled = false;

        
        yield return new WaitForSeconds(attack_cooldown_);
        attacked_ = false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(this.transform.position, attackRadius);
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    GameObject collided_object = collision.gameObject;
    //    if ((collided_object.tag == "Player") && (collided_object != transform.parent.gameObject))
    //    {
    //        PlayerController player_controller_script = collided_object.GetComponent<PlayerController>();
    //        if (!player_controller_script.playerStat.invincible_)
    //        {
    //            player_controller_script.playerStat.TakeDamage(attackDamage);
    //            StartCoroutine(player_controller_script.playerStat.setInvincibility());
    //        }
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerCont = collision.GetComponent<PlayerController>();

        if (playerCont != null && collision.transform.parent != this.transform.parent)
        {
            playerCont.TakeDamage(attackDamage);
        }
    }
}

