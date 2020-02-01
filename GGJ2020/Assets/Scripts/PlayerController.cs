using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStat;


    private bool active = true;
    private AttackBehaviour attackBehaviour;
    private MovementController movementController;

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
}
