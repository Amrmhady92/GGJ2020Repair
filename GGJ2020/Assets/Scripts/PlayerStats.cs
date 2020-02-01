using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SOs/PlayerStats"))]
public class PlayerStats : ScriptableObject
{
    public int playerNumber = 1;
    public int playerHP = 10;
    public int playerMaxHP = 10;


    public bool isDashing = false;


    public bool isPlaying = false;

}

