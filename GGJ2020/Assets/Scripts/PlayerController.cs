using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStat;


    private bool active = true;
    private AttackBehaviour attackBehaviour;
    private MovementController movementController;

    private void Start()
    {
        if(playerStat == null)
        {
            active = false;
            Debug.LogError("No PlayerStat on Player Controller Object");
        }
        attackBehaviour = this.GetComponent<AttackBehaviour>();
        movementController = this.GetComponent<MovementController>();
        if(attackBehaviour == null || movementController == null)
        {
            active = false;
            Debug.LogError("No AttackBehaviour or MovementController on Player Controller Object");
            return;
        }

        
    }
    private void Update()
    {
        if (!active) return;

        if (Input.GetButtonDown("Fire_P"+playerStat.playerNumber))
        {
            attackBehaviour.Attack();
        }

        if (Input.GetButtonDown("Dash_P" + playerStat.playerNumber) && playerStat.isDashing == false)
        {
            movementController.Dash();
        }
    }
}
