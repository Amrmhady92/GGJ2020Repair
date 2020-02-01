using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public Image playerReadyImageOne;
    public Image playerReadyImageTwo;
    public Image playerReadyImageThree;
    public Image playerReadyImageFour;


    public GameObject gameUI;

    public HealthBar playerHpOne;
    public HealthBar playerHpTwo;
    public HealthBar playerHpThree;
    public HealthBar playerHpFour;

    public GameObject backgroundImage;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        if (instance == null) instance = this;
    }

    public void SetPlayerReady(int playerNumber, bool ready)
    {
        switch (playerNumber)
        {
            case 1:
               if(playerReadyImageOne != null) playerReadyImageOne.color = ready ? Color.green : Color.red;
               if(playerHpOne) playerHpOne.gameObject.SetActive(ready);
                break;
            case 2:
                if (playerReadyImageTwo != null) playerReadyImageTwo.color = ready ? Color.green : Color.red;
                if (playerHpTwo) playerHpTwo.gameObject.SetActive(ready);
                break;
            case 3:
                if (playerReadyImageThree != null) playerReadyImageThree.color = ready ? Color.green : Color.red;
                if (playerHpThree) playerHpThree.gameObject.SetActive(ready);
                break;
            case 4:
                if (playerReadyImageFour != null) playerReadyImageFour.color = ready ? Color.green : Color.red;
                if (playerHpFour) playerHpFour.gameObject.SetActive(ready);
                break;
        }
    }


    public void SetPlayerHPUI(PlayerStats pStat)
    {
        switch (pStat.playerNumber)
        {
            case 1:
                playerHpOne.SetValues(pStat.PlayerMaxHP, pStat.PlayerHP);
                break;
            case 2:
                playerHpTwo.SetValues(pStat.PlayerMaxHP, pStat.PlayerHP);
                break;
            case 3:
                playerHpThree.SetValues(pStat.PlayerMaxHP, pStat.PlayerHP);
                break;
            case 4:
                playerHpFour.SetValues(pStat.PlayerMaxHP, pStat.PlayerHP);
                break;
        }
    }

    public void EnableDisableUIScreen(bool enableDisable)
    {
        backgroundImage.SetActive(enableDisable);
        playerReadyImageOne.gameObject.SetActive(enableDisable);
        playerReadyImageTwo.gameObject.SetActive(enableDisable);
        playerReadyImageThree.gameObject.SetActive(enableDisable);
        playerReadyImageFour.gameObject.SetActive(enableDisable);
    }

}
