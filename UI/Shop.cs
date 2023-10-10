using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private int priceTank, money;
    [SerializeField] GameObject targetImage, newImage, currentStatus, newStatus;    

    private bool isBuying; 

    private void Update()
    {   
        if (PlayerPrefs.GetInt("Money") >= priceTank && PlayerPrefs.GetInt("isBuying") != 1)
        {
            gameObject.GetComponent<Button>().interactable = true;           
        }

        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }

        if(PlayerPrefs.GetInt("isBuying") == 1)
        {
            gameObject.GetComponent<Button>().interactable = false;
            currentStatus.SetActive(false);          
            newStatus.SetActive(true);          
        }

        if (PlayerPrefs.GetInt("BuyTank") == 1)
        {
            targetImage.SetActive(false);
            newImage.SetActive(true);
        }
    }

    public void BuyTank()
    {
        if (PlayerPrefs.GetInt("Money") >= priceTank)
        {
            PlayerPrefs.SetInt("BuyTank", 1);
            PlayerPrefs.SetInt("isBuying", 1);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - priceTank);
        }

    }
}
