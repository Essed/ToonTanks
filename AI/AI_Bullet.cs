using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bullet : MonoBehaviour
{
    public float damage; // Урон, который наносит снаряд
    [SerializeField]
    private float speedBullet; // скорость снаряда     
    [SerializeField]
    private float waitTimeToDestroy; // время через которое нужно уничтожить снаряд

    private Rigidbody rb; // ссылка на компонент физики

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // подключаем ссылку на компонент RigidBody
    }

    private void Update()
    {
        MoveBullet(); // двигаем снаряд
    }


    // Метод движения снаряда
    private void MoveBullet()
    {
        rb.AddForce(transform.forward * speedBullet * Time.deltaTime); // Прикладываем к снаряду силу, двигая его вперед

        Destroy(gameObject, waitTimeToDestroy); // уничтожаем через заданное время
    }
}
