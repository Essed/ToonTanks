﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{    
    public void FillBar(float healthPoints)
    {
        gameObject.GetComponent<Image>().fillAmount = healthPoints;
    }   
}
