using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SOs/PlayerStats"))]
public class PlayerStats : ScriptableObject
{

    public System.Action<int> onPlayerDead;
    public System.Action onPlayerHPChange;

    public int playerNumber = 1;
    public int playerIndex = 1;
    [SerializeField] private int playerHP = 10;
    [SerializeField] private int playerMaxHP = 10;


    public bool isPlaying = false;
    public bool isDead = false;
    public int PlayerHP
    {
        get
        {
            return playerHP;
        }

        set
        {
            bool valueChanged = false;
            if (playerHP != value)
            {
                valueChanged = true;
                //Debug.Log("Player Lose HP");
            }
            playerHP = Mathf.Max(0, Mathf.Min( value , playerMaxHP ));
            if(playerHP <= 0)
            {
                onPlayerDead?.Invoke(playerNumber);
                isDead = true;
                //Debug.Log("Player Dead");
            }
            if(valueChanged) onPlayerHPChange?.Invoke();

        }
    }

    public int PlayerMaxHP
    {
        get
        {
            return playerMaxHP;
        }
    }

    
}

