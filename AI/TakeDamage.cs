using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{    
    public float healthPoint; // очки здоровья

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>()) // Если соприкоснулись со снарядом 
        {
            Take_Damage(other.GetComponent<Bullet>().damage); // получить урон           
        }
    }

    // Метод получения урона 
    private void Take_Damage(float damage)
    {
        healthPoint = healthPoint - damage;
    }

}
