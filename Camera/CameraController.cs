using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
       
    // основные параметры
    [SerializeField]
    private float speed; // скорость передвижения
    [SerializeField]
    private float offsetX; // смещение по оси X
    [SerializeField]
    private float offsetZ; // смещение по оси Z   


    // параметры геймплея камеры
    [SerializeField]
    private Transform player; // игрок
    private float movementX; // позиция камеры по оси X
    private float movementZ; // позиция камеры по оси Z   

    private void LateUpdate()
    {
        if (player != null)
        {
            Folow();
        }
       
    }


    // Метод следования за объектов
    private void Folow()
    {
        movementX = player.position.x + offsetX - transform.position.x;
        movementZ = player.position.z + offsetZ - transform.position.z;

        transform.position += new Vector3(movementX * speed * Time.deltaTime, 0, movementZ * speed * Time.deltaTime);
    }

          

}
