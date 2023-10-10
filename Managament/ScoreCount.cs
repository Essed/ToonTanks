using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    [SerializeField]
    private float totalScore; // общее число очков   
    private float score = 0;

    public float scoreFactor; 

    [SerializeField]
    private float chanceSmoothness; // значение от которого зависит шанс выпадения предмета 

    [SerializeField]
    private TextMeshProUGUI scoreTextHUD; // ссылка на текст в HUD 

       

    private void Start()
    {
        scoreTextHUD = FindObjectOfType<TextMeshProUGUI>();
        scoreTextHUD.text = "0";
    }

    public void UpdateScore(float scoreForDeath)
    {
        if (scoreFactor > 0)
        {
            scoreForDeath = scoreForDeath * scoreFactor;
        }

        score = score + scoreForDeath;
        
        while(totalScore <= score)
        {
            scoreTextHUD.text = totalScore.ToString();
            totalScore++;
            
        }       
    }    

    public int ChanceDropItem()
    {
        if (totalScore % chanceSmoothness == 0) return 1;

        else return 0;
    }
}
