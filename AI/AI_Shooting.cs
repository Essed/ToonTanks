using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shooting : MonoBehaviour
{
    [SerializeField]
    private float attackDistance; // дистанция, с которой нужно реагировать на цель
    [SerializeField]
    private float timePeriod; // период времени для подсчета скорострельности
    [SerializeField]
    private float Rpt; // обороты
    private float secondBetweenShot; // время между выстрелами
    private float nextTimeShot; // время до следующего выстрела


    [SerializeField]
    private GameObject bullet; // префаб снаряда
    [SerializeField]
    private Transform spawnPointBullet; // место спавна снарядов

    
    private void Update()
    {
        secondBetweenShot = timePeriod / Rpt; // высчитываем время между выстрелами

        Attack();
    }

    // Метод атаки
    private void Attack()
    {
        if (ShootDelay()) // Если можно стрелять
        {
            RaycastHit hit; // создать луч

            if (Physics.Raycast(spawnPointBullet.position, spawnPointBullet.forward, out hit, attackDistance)) // Если луч попал на твердое тело
            {                
                if (hit.transform.GetComponent<PlayerController>()) // Если луч попал в цель
                {                    
                    Instantiate(bullet, spawnPointBullet.position, spawnPointBullet.rotation); // спавним снаряд на месте спавна                     
                }
            }       

            nextTimeShot = Time.time + secondBetweenShot; // увеличиваем время до следующего выстрела
        }
    }

    // Метод создающий задержку между выстрелами
    private bool ShootDelay()
    {
        bool delay = true; // флаг 

        if (Time.time < nextTimeShot) // Если время до следующего выстрела еще не наступило
        {
            delay = false; // выключить флаг
        }

        return delay; // вернуть флаг задержки
    }
}
