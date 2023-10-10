using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBar : MonoBehaviour
{
    public void FillBar(float spellPoints)
    {
        gameObject.GetComponent<Image>().fillAmount = spellPoints;
    }
}
