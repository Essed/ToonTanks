using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectBar : MonoBehaviour
{
       
    public float FillBar(float spellPoints)
    {
        GetComponent<Image>().fillAmount = spellPoints;

        if(GetComponent<Image>().fillAmount == 0)
            return GetComponent<Image>().fillAmount;

        return GetComponent<Image>().fillAmount;
    }
}
