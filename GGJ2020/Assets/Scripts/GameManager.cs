﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[Range(0,1f)]
    //public float repairSequenceMinHPAll = 0.5f;
    //[Range(0, 1f)]
    //public float repairSequenceMinHPIndividual = 0.5f;
    public int repairCountDown = 5;
    public int repairAmount = 5;
    public int healTimer = 10;

    public PlayerStats[] playerStats;
    public GameObject[] playersPrefabs; 

    public Image greenEffectImage;
    public Color healEffectColor;

    public bool playerReadyOne = false;
    public bool playerReadyTwo = false;
    public bool playerReadyThree = false;
    public bool playerReadyFour = false;

    public float unitSpawnSize = 4;

    public KandooZ.CountDowner countDowner;

    private bool canReady = true;

    private List<PlayerStats> currentPlayersStats;
    private PlayerController[] playerControllers;
    int readyPlayerCount;
    bool startMenu = true;
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
        
        for (int i = 0; i < playerStats.Length; i++)
        {
            playerStats[i].PlayerHP = playerStats[i].PlayerMaxHP;
            playerStats[i].isDead = false;
        }
    }

    private void Update()
    {
        if (startMenu)
        {
            if ((Input.GetButtonDown("Fire_P1") || Input.GetKeyDown(KeyCode.R)) && canReady)
            {
                playerReadyOne = !playerReadyOne;
                if (playerReadyOne) readyPlayerCount++;
                else readyPlayerCount--;
                UIManager.Instance.SetPlayerReady(1, playerReadyOne);
            }
            if ((Input.GetButtonDown("Fire_P2") || Input.GetKeyDown(KeyCode.R)) && canReady)
            {
                playerReadyTwo = !playerReadyTwo;
                if (playerReadyTwo) readyPlayerCount++;
                else readyPlayerCount--;
                UIManager.Instance.SetPlayerReady(2, playerReadyTwo);
            }
            if ((Input.GetButtonDown("Fire_P3") || Input.GetKeyDown(KeyCode.R)) && canReady)
            {
                playerReadyThree = !playerReadyThree;
                if (playerReadyThree) readyPlayerCount++;
                else readyPlayerCount--;
                UIManager.Instance.SetPlayerReady(3, playerReadyThree);
            }
            if (Input.GetButtonDown("Fire_P4") && canReady)
            {
                playerReadyFour = !playerReadyFour;
                if (playerReadyFour) readyPlayerCount++;
                else readyPlayerCount--;
                UIManager.Instance.SetPlayerReady(4, playerReadyFour);
            }

            if (Input.GetButtonDown("Start") && canReady)
            {
                if (readyPlayerCount >= 2)
                {
                    canReady = false;
                    startMenu = false;
                    StartGame();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnRestartButtonPressed();
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

            GameObject playerObejct;
            for (int i = 0; i < playersPrefabs.Length; i++)
            {
                if(playerStats[i].isPlaying)
                {
                    playerObejct = GameObject.Instantiate(playersPrefabs[i]);
                    playerObejct.transform.position = UnityEngine.Random.insideUnitCircle * unitSpawnSize;
                    playerObejct.GetComponent<PlayerController>().Active = false;
                }
            }

            UIManager.Instance.EnableDisableUIScreen(false);
            UIManager.Instance.gameUI.SetActive(true);
            currentPlayersStats = new List<PlayerStats>();
            countDowner.StartCountDown(() => 
            {
                playerControllers = GameObject.FindObjectsOfType<PlayerController>();
                for (int i = 0; i < playerControllers.Length; i++)
                {
                    playerControllers[i].Active = true;
                    UIManager.Instance.SetPlayerHPUI(playerControllers[i].playerStat);
                    playerControllers[i].playerStat.onPlayerHPChange += OnPlayerHit;
                    playerControllers[i].playerStat.onPlayerDead += OnPlayerDead;
                    currentPlayersStats.Add(playerControllers[i].playerStat);
                    //Sound here
                    StartCoroutine(HealingTimer());
                }
            });

        }
        else
        {
            Debug.LogError("PlayerStats list is wrong");
            return;
        }




    }

    private IEnumerator HealingTimer()
    {
        yield return new WaitForSeconds(healTimer);

        alivePlayers = 0;
        for (int i = 0; i < currentPlayersStats.Count; i++)
        {
            if (currentPlayersStats[i].isDead) continue;
            alivePlayers++;
        }
        if (alivePlayers > 2)
        {
            Debug.Log("Begin HealSequence");
            BeginRepairSequence();
        }
    }

    float maxHPs;
    float currentHPs;
    bool individualUnderMinHP = false;
    int alivePlayers;
    private bool repairSequenceUnderWay = false;
    public void OnPlayerHit()
    {
        //Debug.Log("Player hit");
        for (int i = 0; i < playerStats.Length; i++)
        {
            UIManager.Instance.SetPlayerHPUI(playerStats[i]);
        }
        //maxHPs = 0;
        //currentHPs = 0;
        //individualUnderMinHP = false;
        //alivePlayers = 0;
        //for (int i = 0; i < currentPlayersStats.Count; i++)
        //{
        //    if (currentPlayersStats[i].isDead) continue;
        //    alivePlayers++;
        //    maxHPs += (float)currentPlayersStats[i].PlayerMaxHP;
        //    currentHPs += (float)currentPlayersStats[i].PlayerHP;
        //    if(!individualUnderMinHP) individualUnderMinHP = ((float)currentPlayersStats[i].PlayerHP / (float)currentPlayersStats[i].PlayerMaxHP) <= repairSequenceMinHPIndividual;
        //    Debug.Log(currentPlayersStats[i].PlayerHP / currentPlayersStats[i].PlayerMaxHP);
        //}
        //Debug.Log("maxHPs = " + maxHPs + "\ncurrentHPs = " + currentHPs + "\nindividualUnderMinHP = " + individualUnderMinHP);
        //if ((currentHPs / maxHPs <= repairSequenceMinHPAll || individualUnderMinHP) && alivePlayers > 2) 
        //{
        //    Debug.Log("Begin HealSequence");
        //    BeginRepairSequence();
        //}

    }

    private void BeginRepairSequence()
    {
        if (repairSequenceUnderWay) return;
        repairSequenceUnderWay = true;

        countDowner.countDownPhrase = "REPAIR!";
        countDowner.countDownTimes = repairCountDown;
        countDowner.StartCountDown(()=>
        {
            Debug.Log("HEALING");
            PlayerController[] playerControllers = GameObject.FindObjectsOfType<PlayerController>();
            for (int i = 0; i < playerControllers.Length; i++)
            {
                if(playerControllers[i].playerStat.isPlaying && !playerControllers[i].playerStat.isDead)
                {
                    playerControllers[i].HealEffect();
                }
            }
            if(greenEffectImage != null )
            {
                LeanTween.cancel(greenEffectImage.gameObject);
                LeanTween.color(greenEffectImage.rectTransform, healEffectColor, 0.5f).setOnComplete(() =>
                {
                    LeanTween.color(greenEffectImage.rectTransform, new Color(0, 0, 0, 0), 0.5f);
                });
            }
            repairSequenceUnderWay = false;
            StartCoroutine(HealingTimer());
        });

    }

    public void OnPlayerDead(int playerNumber)
    {
        for (int i = 0; i < playerControllers.Length; i++)
        {
            if(playerControllers[i].playerStat.playerNumber == playerNumber)
            {
                playerControllers[i].gameObject.SetActive(false);
                //Play Explosion Effect
                break;
            }
        }
    }

    public void OnRestartButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
