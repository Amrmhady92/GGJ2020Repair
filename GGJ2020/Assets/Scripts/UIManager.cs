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
                break;
            case 2:
                if (playerReadyImageTwo != null) playerReadyImageTwo.color = ready ? Color.green : Color.red;
                break;
            case 3:
                if (playerReadyImageThree != null) playerReadyImageThree.color = ready ? Color.green : Color.red;
                break;
            case 4:
                if (playerReadyImageFour != null) playerReadyImageFour.color = ready ? Color.green : Color.red;
                break;
        }
    }


    public void EnableDisableBG(bool enableDisable)
    {
        backgroundImage.SetActive(enableDisable);
    }

}
