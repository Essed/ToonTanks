using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    private GameObject[] elementsShow;
    [SerializeField]
    private GameObject[] elementsHide;  

    public void LoadLevel_1()
    {
        if (PlayerPrefs.GetInt("TankID") == 1)
        {
            SceneManager.LoadScene("Level_1_Def");
        }

        if(PlayerPrefs.GetInt("TankID") == 2)
        {
            SceneManager.LoadScene("Level_1_New");
        }

        Time.timeScale = 1;
    }
        
    public void LoadLevel_2()
    {
        if (PlayerPrefs.GetInt("TankID") == 1)
        {
            SceneManager.LoadScene("Level_2_Def");
        }

        if (PlayerPrefs.GetInt("TankID") == 2)
        {
            SceneManager.LoadScene("Level_2_New");
        }

        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {      
        Application.Quit();       
        Time.timeScale = 1;
    }

    public void PauseOn()
    {
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
    }

    public void SwitchTank_1()
    {
        PlayerPrefs.SetInt("TankID", 1);
    }

    public void SwitchTank_2()
    {
        PlayerPrefs.SetInt("TankID", 2);
    }

    public void HideAndShowElements()
    {
        for (int i = 0; i < elementsHide.Length; i++)
        {
            elementsHide[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < elementsShow.Length; i++)
        {
            elementsShow[i].gameObject.SetActive(true);
        }
    }

}
