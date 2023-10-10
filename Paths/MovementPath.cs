using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes // виды пути: линейный или зацикленный
    {
        linear,
        loop
    }

    public PathTypes pathType; // тип пути
    public int movementDirection = 1; // направление движение: вперед или назад
    public int moveingTo = 0; // к какой точке двигаться

    public Transform[] pathElements; // массив из точек движения

    public void OnDrawGizmos() // отображает линии между точками пути
    {
        if(pathElements == null || pathElements.Length < 2) // проверяет есть ли хотя бы 2 элемента пути
        {
            return; 
        }

        for (var i = 1;  i < pathElements.Length; i++) // прогоняет все точки массива
        {
            Gizmos.DrawLine(pathElements[i - 1].position, pathElements[i].position); // рисует линии между ними
        }   

        if(pathType == PathTypes.loop) // что произойдет если путь замкнется 
        {
            Gizmos.DrawLine(pathElements[0].position, pathElements[pathElements.Length - 1].position); // нарисовать линии от последней точки к первой
        }
    }


    public IEnumerator<Transform> GetNextPathPoint() // получает положение следующей точки
    {
        if (pathElements == null || pathElements.Length < 1) // проверяется, есть ли точки которым нужно проверять положение
        {
            yield break; // позволяет выйти из корутины если нашел несоответствие
        }

        while (true)
        {
            yield return pathElements[moveingTo]; // возвращает текущее положение точки

            if (pathElements.Length == 1) // если точка всего одна, выйти
            {
                continue;
            }

            if (pathType == PathTypes.linear) // если линия не зациклена
            {
                if(moveingTo <= 0) // если двигаемся по нарастающей 
                {
                    movementDirection = 1; // добавлем один к движению
                }

                else if(moveingTo >= pathElements.Length - 1) // если двигаемся по убывающей
                {
                    movementDirection = -1; // убираем один из движения
                }
            }

            moveingTo = moveingTo + movementDirection; // диапазон движение от 1 до -1

            if (pathType == PathTypes.loop) // если линия зациклена
            {
                if(moveingTo >= pathElements.Length) // если мы дошли до последней точки
                {
                    moveingTo = 0; // то надо идти, не в обратную сторону, а к первой точки
                }

                if (moveingTo < 0) // если мы дошли до первой точки двигаясь в обратную сторону
                {
                    moveingTo = pathElements.Length - 1; // то надо двигаться к последней
                }
            }
        }
    }

}
