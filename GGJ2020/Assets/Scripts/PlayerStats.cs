using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SOs/PlayerStats"))]
public class PlayerStats : ScriptableObject
{

    public System.Action<int> onPlayerDead;
    public System.Action onPlayerLoseHP;

    public int playerNumber = 1;
    [SerializeField] private int playerHP = 10;
    [SerializeField] private int playerMaxHP = 10;
    public bool invincible_ = false;
    public float invincibility_timer_ = 0.5f;

    public bool isPlaying = false;

    public int PlayerHP
    {
        get
        {
            return playerHP;
        }

        set
        {
            if (playerHP > value)
            {
                onPlayerLoseHP?.Invoke();
                //Debug.Log("Player Lose HP");

            }
            playerHP = Mathf.Max(0, Mathf.Min( value , playerMaxHP ));
            if(playerHP <= 0)
            {
                onPlayerDead?.Invoke(playerNumber);
                //Debug.Log("Player Dead");
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

    public void TakeDamage(int damage_taken) { PlayerHP -= damage_taken; }

    public IEnumerator setInvincibility()
    {
        invincible_ = true;
        yield return new WaitForSeconds(invincibility_timer_);
        invincible_ = false;
    }
}

