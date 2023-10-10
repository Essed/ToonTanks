using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private float rangeAttack; // дальность атаки 
   
    private float nextTimeShot; // время до следующего выстрела
    private float secondBetweenShot; // время между выстрелами

    [SerializeField]
    private float timePeriod; // период времени для подсчета скорострельности
    [SerializeField]
    private float Rpt; // обороты

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform gunTower;

    [SerializeField]
    private Transform spawnPointBullet; // место спавна снарядов


    private void Awake()
    {
        secondBetweenShot = timePeriod / Rpt;
    }

    private void Update()
    {        
        Shoot();
    }


    private void Shoot()
    {
        if (ShootDelay())
        {
            RaycastHit hit;

            if (Physics.Raycast(spawnPointBullet.position, spawnPointBullet.forward, out hit, rangeAttack))
            {
                if (hit.transform.tag == "Enemy")
                {
                    Instantiate(bullet, spawnPointBullet.position, spawnPointBullet.rotation);
                }
            }

            nextTimeShot = Time.time + secondBetweenShot; // увеличиваем время до следующего выстрела
        }
    }


    // Задержка между выстрелами
    private bool ShootDelay()
    {
        bool delay = true;

        if(Time.time < nextTimeShot)
        {
            delay = false;
        }

        return delay;

    }
}
