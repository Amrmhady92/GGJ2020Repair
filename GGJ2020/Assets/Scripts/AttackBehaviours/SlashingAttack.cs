using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashingAttack : AttackBehaviour
{
    public Collider2D attack_box_;
    
    public float attack_duration_;
    public bool attacked_ = false;

    private void Start()
    {
        attack_box_ = GetComponent<Collider2D>();
        attack_box_.enabled = false;
    }

    public override void Attack()
    {
        if (!attacked_)
        {
            StopAllCoroutines();
            StartCoroutine(ActivateAttack());
        }
    }

    private IEnumerator ActivateAttack()
    {
        attacked_ = true;
        attack_box_.enabled = true;
        yield return new WaitForSeconds(attack_duration_);
        attack_box_.enabled = false;
        yield return new WaitForSeconds(attack_cooldown_);
        attacked_ = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (collision.gameObject != transform.parent.gameObject))
        {
            Debug.Log("Hit a player");
        }
    }
}