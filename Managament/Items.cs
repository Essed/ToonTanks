using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType
    {
        HealthPoint,
        DoubleScore,
        PowerUp
    }
    
    public ItemType Type; // тип предмета

    public float timeAction; // время действия  
    
    [SerializeField]
    private float hpRecovery; // значение восполнения здоровья  
    [SerializeField]
    private float factorScore; // множитель очков
    [SerializeField]
    private Bullet p_Bullet; // префаб снаряда
    
    ScoreCount scoreCounter; // ссылка на счетчик очков

    private void Start()
    {      
      scoreCounter = GameObject.Find("Score Counter").GetComponent<ScoreCount>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerController>())
        {
            if (Type == ItemType.HealthPoint)
            {
                // восстановить здоровье
                other.transform.GetComponent<PlayerController>().healthPoint = other.transform.GetComponent<PlayerController>().healthPoint + hpRecovery;
                Destroy(gameObject);
            }

            if (Type == ItemType.DoubleScore)
            {
                scoreCounter.scoreFactor = factorScore;
                Destroy(gameObject);
            }

            if (Type == ItemType.PowerUp)
            {              
                p_Bullet.damage = p_Bullet.damage + 20;
                Destroy(gameObject);
            }   
        }
    }

}
