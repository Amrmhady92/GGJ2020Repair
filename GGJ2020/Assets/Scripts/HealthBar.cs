using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Image filledImage;
    public TMPro.TextMeshProUGUI playerText;

    public Color goodHP;
    public Color medHP;
    public Color lowHP;

    public void SetValues(float max, float newvalue)
    {
        float percent = (newvalue / max);
        percent = Mathf.Max(0, Mathf.Min(percent,1));


        if(percent < 0.3f)
        {
            filledImage.color = lowHP;
        }
        else if(percent < 0.6f)
        {
            filledImage.color = medHP;
        }
        else
        {
            filledImage.color = goodHP;
        }

        filledImage.fillAmount = percent;
    }

}
