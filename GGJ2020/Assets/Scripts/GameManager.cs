using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerStats[] playerStats;
    public GameObject[] playersPrefabs; 

    public bool playersReady = true;

    public bool playerReadyOne = false;
    public bool playerReadyTwo = false;
    public bool playerReadyThree = false;
    public bool playerReadyFour = false;

    public bool canReady = true;

    int readyPlayerCount;
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire_P1") && canReady)
        {
            playerReadyOne = !playerReadyOne;
            if (playerReadyOne) readyPlayerCount++;
            else readyPlayerCount--;
        }
        if (Input.GetButtonDown("Fire_P2") && canReady)
        {
            playerReadyTwo = !playerReadyTwo;
            if (playerReadyTwo) readyPlayerCount++;
            else readyPlayerCount--;
        }
        if (Input.GetButtonDown("Fire_P3") && canReady)
        {
            playerReadyThree = !playerReadyThree;
            if (playerReadyThree) readyPlayerCount++;
            else readyPlayerCount--;

        }
        if (Input.GetButtonDown("Fire_P4") && canReady)
        {
            playerReadyFour = !playerReadyFour;
            if (playerReadyFour) readyPlayerCount++;
            else readyPlayerCount--;
        }

        if (Input.GetButtonDown("Start") && canReady)
        {
            if(readyPlayerCount >= 2)
            {
                canReady = false;
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        if(playerStats.Length == 4 && playersPrefabs.Length == 4)
        {
            playerStats[0].isPlaying = playerReadyOne;
            playerStats[1].isPlaying = playerReadyTwo;
            playerStats[2].isPlaying = playerReadyThree;
            playerStats[3].isPlaying = playerReadyFour;



        }
        else
        {
            Debug.LogError("PlayerStats list is wrong");
            return;
        }



    }
}
