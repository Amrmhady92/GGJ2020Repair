﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class MovementController : MonoBehaviour
{
    public float speed = 1;
    [Space(10)]
    public float dashForce = 5f;
    public float dashDuration = 1f;
    public float dashCoolDown = 1.5f;
    public bool isDashing = false;
    public bool canDash = true;
    public GameObject dasheffect;
    private float x, y = 0;
    private float xIn, yIn = 0;
    private float angle;


    private int playerNumber = 1;

    private Rigidbody2D rb;


    private bool active = true;

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerNumber = this.GetComponent<PlayerController>().playerStat.playerNumber;
    }

    void Update()
    {
        if (!active) return;

        if (isDashing) return;


        xIn = Input.GetAxis("LeftAxisH_P" + playerNumber);
        yIn = Input.GetAxis("LeftAxisV_P" + playerNumber);
        x = Mathf.Abs(xIn) < 0.05f ? x : xIn;
        y = Mathf.Abs(yIn) < 0.05f ? y : yIn;

        if(xIn != 0 || yIn != 0)
        {
            angle = Mathf.Atan2(-x, y) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
         rb.velocity = new Vector2(xIn,yIn) * speed;
    }

    public void Dash()
    {
        if (isDashing) return;
        if (!canDash) return;


        isDashing = true;
        canDash = false;

        rb.velocity = Vector2.zero;
        if (dasheffect != null) dasheffect.SetActive(true);
        rb.AddForce(this.transform.up * dashForce,ForceMode2D.Impulse);
        StopAllCoroutines();
        StartCoroutine(DoAfter(() => 
        {
            rb.velocity = Vector2.zero;
            isDashing = false;
            if (dasheffect != null) dasheffect.SetActive(false);

        },
        dashDuration));
        StartCoroutine(DoAfter(() => { canDash = true; }, dashCoolDown));
        FindObjectOfType<AudioManager>().Play("Dash" + GetComponent<PlayerController>().playerStat.playerNumber);
    }

    private IEnumerator DoAfter(System.Action callback, float time) 
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

}
