using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SOs/PlayerStats"))]
public class PlayerStats : ScriptableObject
{

    System.Action onPlayerDead;
    System.Action onPlayerLoseHP;

    public int playerNumber = 1;
    private int playerHP = 10;
    private int playerMaxHP = 10;

    public bool isPlaying = false;

    public int PlayerHP
    {
        get
        {
            return playerHP;
        }

        set
        {
            if (playerHP != value)
            {
                onPlayerLoseHP?.Invoke();
            }
            playerHP = Mathf.Max(0, value);
            if(playerHP <= 0)
            {
                onPlayerDead?.Invoke();
            }

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

