using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    private float actionTime; // время действия способности
    private float currentTime; // текущее время действия


    [SerializeField]
    private float timePeriod; // период времени для подсчета скорострельности
    [SerializeField]
    private float Rpt; // обороты
    private float secondBetweenShot; // время между выстрелами
    private float nextTimeShot; // время до следующего выстрела

    [SerializeField]
    private GameObject[] spawnPointBullet; // места спавна снарядов 
    [SerializeField]
    private GameObject p_Bullet; // префаб снаряда 

    private void Start()
    {
        currentTime = actionTime;
    }

    private void Update()
    {
        secondBetweenShot = timePeriod / Rpt; // высчитываем время между выстрелами

        Attack();        

        SelfDestroy();
    }

    // Метод спавна способности 
    private void Attack()
    {
        if (ShootDelay())
        {
            for (int i = 0; i < spawnPointBullet.Length; i++)
            {
                Instantiate(p_Bullet, spawnPointBullet[i].transform.position, spawnPointBullet[i].transform.rotation);
                currentTime--;
            }

            nextTimeShot = Time.time + secondBetweenShot; // увеличиваем время до следующего выстрела
        }
        
    }

    // Метод создающий задержку между выстрелами
    private bool ShootDelay()
    {
        bool delay = true; // флаг задержки

        if (Time.time < nextTimeShot) // Если время до следующего выстрела еще не наступило
        {
            delay = false; // выключить флаг
        }

        return delay; // вернуть флаг задержки
    }

    // Метод уничтожения по истечению времени действия 
    private void SelfDestroy()
    {
        if (currentTime <= 0)
        {
            Destroy(gameObject);
        }
    }

}
