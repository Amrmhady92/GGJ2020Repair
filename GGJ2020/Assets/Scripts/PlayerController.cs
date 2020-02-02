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

    public SpriteRenderer shipSpriteRenderer;
    public Sprite[] shipStateSprites;
    public Sprite[] shipPartsSprites;
    public GameObject shiptSpritePartPrefab;
    public float minPartSpeed = 15f;
    public float maxPartSpeed = 25f;
    private Pooler shipPartsPooler;
    
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

        shipPartsPooler = this.gameObject.AddComponent<Pooler>();
        shipPartsPooler.startAmount = 2;
        shipPartsPooler.PooledObject = shiptSpritePartPrefab;

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
    float percent;
    GameObject partFalling;

    public void TakeDamage(int damage_taken)
    {
        if (invincible_) return;

        playerStat.PlayerHP -= damage_taken;
        StopAllCoroutines();
        if (this.gameObject.activeSelf) StartCoroutine(setInvincibility());
        SetShipSprite();
        if (partFalling != null)
        {
            partFalling.transform.position = this.transform.position;
            partFalling.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(minPartSpeed, maxPartSpeed), ForceMode2D.Force);
            partFalling.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(1f, 3f);
            partFalling.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            LeanTween.cancel(partFalling);
            LeanTween.scale(partFalling, Vector2.zero, 1.5f).setOnComplete(() => { partFalling.SetActive(false); });

        }
    }

    private void SetShipSprite()
    {
        if (shipStateSprites.Length == 4)
        {
            percent = ((float)playerStat.PlayerHP / (float)playerStat.PlayerMaxHP);
            percent = Mathf.Max(0, Mathf.Min(percent, 1));
            if (percent < 0.3f)
            {
                shipSpriteRenderer.sprite = shipStateSprites[3];
                partFalling = shipPartsPooler.Get(true);
                partFalling.GetComponent<SpriteRenderer>().sprite = shipPartsSprites[2];
            }
            else if (percent < 0.6f)
            {
                shipSpriteRenderer.sprite = shipStateSprites[2];
                partFalling = shipPartsPooler.Get(true);
                partFalling.GetComponent<SpriteRenderer>().sprite = shipPartsSprites[1];
            }
            else if (percent < 0.8f)
            {
                shipSpriteRenderer.sprite = shipStateSprites[1];
                partFalling = shipPartsPooler.Get(true);
                partFalling.GetComponent<SpriteRenderer>().sprite = shipPartsSprites[0];
            }
            else
            {
                partFalling = null;
                shipSpriteRenderer.sprite = shipStateSprites[0];
            }


        }
        else
        {
            Debug.Log("Not enough sprites");
        }
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
