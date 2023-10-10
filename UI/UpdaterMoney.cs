using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdaterMoney : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    [SerializeField] private Money _moneyText;
    [SerializeField] private Button changeButton, lvlButton;
    [SerializeField] private GameObject newImageAngar, currentImageAngar, defaultTank, newTank;
    [SerializeField] private Image lvlImg;

    private void Start()
    {        
        PlayerPrefs.SetInt("OpenLoc", 1);
        PlayerPrefs.SetInt("isBuying", 1);
    }    

    private void Update()
    {
        moneyText.text = PlayerPrefs.GetInt("Money").ToString();

        if (PlayerPrefs.GetInt("isBuying") == 1)
        {
            currentImageAngar.SetActive(false);   
            newImageAngar.SetActive(true);
            changeButton.interactable = true;
        }

        if(PlayerPrefs.GetInt("TankID") == 1)
        {
            newTank.SetActive(false);
            defaultTank.SetActive(true);            
        }

        if (PlayerPrefs.GetInt("TankID") == 2)
        {
            defaultTank.SetActive(false);
            newTank.SetActive(true);
        }

        if(PlayerPrefs.GetInt("OpenLoc") == 1)
        {
            lvlImg.color = Color.white;
            lvlButton.interactable = true;
        }
        else
        {
            lvlImg.color = Color.black;
            lvlButton.interactable = false;
        }

    }
}
