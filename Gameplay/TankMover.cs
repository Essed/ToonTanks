using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed; // скорость

    [SerializeField]
    private float rotationSpeed; // скорость поворота

    [SerializeField]
    private float sensivity; // чувствительность

    private Vector3 moveVector; // направление движения 
    private Vector3 playerDirection; // направление поворота    


    [SerializeField]
    private Transform gun_Tower; // башня
    private CharacterController ch_controller; // контроллер
    private Joystick lft_Joytstick; // левый джойстик
    private Thumbstick rgt_Joystick; // правый джойстик
    [SerializeField]
    private Rigidbody rb; // физическое тело


    private void Start()
    {
        SetLinks();       
    }

    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }


    // Метод перемещения танка
    private void Move()
    { 
        moveVector = Vector3.zero;
        moveVector.x = lft_Joytstick.Horizontal() * moveSpeed;
        moveVector.z = lft_Joytstick.Vertical() * moveSpeed;      

        if(Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, rotationSpeed, 0.0f);

            Quaternion lookRotation = Quaternion.LookRotation(direct.normalized, Vector3.up); // получение нужного поворота к позиции       

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); // поворот
        }

        rb.AddForce(moveVector.normalized * Time.fixedDeltaTime * moveSpeed);
    }



    // Метод поворота башни
    private void Rotate()
    {
        playerDirection = Vector3.right * rgt_Joystick.Horizontal() + Vector3.forward * rgt_Joystick.Vertical();

        if (playerDirection.sqrMagnitude != 0.0f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            gun_Tower.rotation = Quaternion.Lerp(gun_Tower.rotation,lookRotation, Time.deltaTime * sensivity);                       
        }
        
    }   

    // Метод подключения всех ссылок
    private void SetLinks()
    {
        ch_controller = GetComponent<CharacterController>();
        lft_Joytstick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        rgt_Joystick = GameObject.FindGameObjectWithTag("Thumbstick").GetComponent<Thumbstick>();
        rb = GetComponent<Rigidbody>();
    }
}


