using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed; // скорость движения

    [HideInInspector]
    public float turnSpeed; // скорость поворота

    [SerializeField]
    private float maxDistance; // на какое расстояние объект должен подъехать к точке, чтобы понять что это точка

    public enum MovementType
    {
        Moveing,
        Lerping
    }

    public MovementType Type = MovementType.Moveing; // вид движения
    public MovementPath myPath; // используемый путь 

    private IEnumerator<Transform> PointInPath; // проверка точек

    private void Start()
    {
        if (myPath == null) // проверка прикреплен ли путь
        {
            return;
        }

        PointInPath = myPath.GetNextPathPoint(); // обращение к корутине GetNextPathPoint

        PointInPath.MoveNext(); // получение следующей точки в пути

        if (PointInPath.Current == null) // если точка к которой передвигаться
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, PointInPath.Current.position, Time.deltaTime * moveSpeed); // объект должен стать на стартовую точку пути
    }

    private void Update()
    {
        if(PointInPath == null || PointInPath.Current == null) // проверка отсутствия пути 
        {
            return; // выход, пути нет
        } 

        if(Type == MovementType.Moveing) // если выбран этот вид 
        {            
            Vector3 direction = PointInPath.Current.position - transform.position; // направление к следующей точке 
            
            if(direction.sqrMagnitude != 0.0f) // если не нулевое направление
            { 
                Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up); // получение нужного поворота к точке

                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed); // поворот

                transform.position = Vector3.MoveTowards(transform.position, PointInPath.Current.position, Time.deltaTime * moveSpeed); // движение

            }
        }

        else if(Type == MovementType.Lerping) // если выбран этот вид 
        {
            Vector3 direction = PointInPath.Current.position - transform.position; // направление к следующей точке 

            if (direction.sqrMagnitude != 0.0f) // если не нулевое направление
            {

                Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up); // получение нужного поворота к точке

                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed); // поворот

                transform.position = Vector3.MoveTowards(transform.position, PointInPath.Current.position, Time.deltaTime * moveSpeed); // движение

            }
        }

        var distanceSquare = (transform.position - PointInPath.Current.position).sqrMagnitude; // достаточно ли мы близко, чтобы начать двигаться к следующей точки

        if(distanceSquare < maxDistance * maxDistance) // достаточно ли мы близко по теореме Пифагора 
        {
            PointInPath.MoveNext(); // двигаться к следующей точке 
        }
    }

}
