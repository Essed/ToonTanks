using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Tank : MonoBehaviour
{    
    [SerializeField]
    private float scoreForDeath; // очки за смерть
    [SerializeField]
    private float waitTimeDeath; // задержка времени перед уничтожением при смерти  
    [SerializeField]
    private float moveSpeed; // скорость движения 
    [SerializeField]
    private float turnSpeed; // скорость поворота танка
    [SerializeField]
    private float turnSpeedTower; // скорость поворота башни
    [SerializeField]
    private float attackDistance; // дистанция, с которой нужно реагировать на цель
    [SerializeField]
    private float distanceToWayPoint; // расстояние до начальной позиции  
    private float currentDistance; // расстояние до начальной позиции


    [SerializeField]
    private float offsetX; // смещение по оси X для вычисления места спавна стартовой позиции 
    [SerializeField]
    private float offsetZ; // смещение по оси Z для вычисления места спавна стартовой позиции
    private bool  isMovingOnPath; // флаг, движется ли танк по основному пути  

    [SerializeField]
    private float dropChance; // шанс выпадения предмета


    [SerializeField]
    private Transform startWayPoint; // начальная позиция, на которую танк подъедет
    [SerializeField]
    private Transform target; // начальная позиция, на которую танк подъедет
    [SerializeField]
    private Transform gunTower; // башня, которую нужно поворачивать в сторону цели
    [SerializeField]
    private GameObject p_Tank; // префаб танка
    [SerializeField]
    private Transform spawnPathPoint; // точка в которой нужно заспавнить путь для движения танка   
    [SerializeField]
    private GameObject p_WayPoint; // префаб стартовой позиции для спавна
    private ScoreCount scoreCounter; // ссылка на счетчик очков
    private Rigidbody rb; // ссылка на компонент RigidBody      
    private TakeDamage takeDamageComponent; // ссылка на компонент TakeDamage
    private DropLoot dropLootComponent; // ссылка на компонент DropLoot


    // Камера шейкер
    [SerializeField]
    private float power;
    [SerializeField]
    private float slowDownAmount;   
    private bool shouldShake;
    private CameraShake camShake; // ссылка на компонент CameraShake камеры


    // Вэйпоинты
    [SerializeField]
    private GameObject[] wayPoints; // массив с вэйпоинтами
    [SerializeField]
    private float wp_Radius; // минимальное сближение с вэйпоинтом
    private int currentWayPoint = 0; // текущий вэйпоинт


    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>(); // найти цель      
        rb = GetComponent<Rigidbody>(); // подключить ссылку на компонент RigidBody   
        dropLootComponent = GetComponent<DropLoot>(); // подключить ссылку на компонент RigidBody 
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        takeDamageComponent = transform.GetChild(0).GetComponent<TakeDamage>(); // подключение компонента TakeDamage для получения урона
        scoreCounter = GameObject.Find("Score Counter").GetComponent<ScoreCount>(); // получить счетчик очков
        startWayPoint = SpawnStartWayPoint().GetComponent<Transform>(); // получить стартовую позицию для танка
    }

    private void Update()
    {
        if (takeDamageComponent.healthPoint <= 0)
        {
            DestroyAndUpScore(); // уничтожить танк и начислить очки     
            Destroy(startWayPoint.gameObject);
        }       
    }

    private void FixedUpdate()
    {
        if (takeDamageComponent.healthPoint > 0)
        {
            if (target != null)
            {
                currentDistance = Vector3.Distance(target.position, transform.position); // вычисление текущей дистанции между целью и танком

                MoveToStarPoint(); // двигаться к стартовой позиции

                MoveOnWayPoints(); // двигаться по пути 

                if (currentDistance <= attackDistance) // если текущая дистанция такая же или меньше чем дистанция реакции
                {
                    LookToTarget(target, gunTower); // повернуть башню в сторону игрока                   
                }
            }

        }        
    }


    // Метод движение к начальной позиции 
    private bool MoveToStarPoint()
    {
        if (isMovingOnPath == false) // если танк еще не доехал до стартовой позиции
        {
            distanceToWayPoint = (transform.position - startWayPoint.position).sqrMagnitude; // дистанция до стартовой позиции

            Vector3 direction = startWayPoint.position - transform.position; // направление к позиции для поворота

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z)); // получение нужного поворота к позиции       

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.fixedDeltaTime * turnSpeed); // поворот            

            transform.position = Vector3.MoveTowards(transform.position, startWayPoint.position, Time.fixedDeltaTime * moveSpeed); // движение танка к стартовой позиции

            if (distanceToWayPoint < 0.1f) // достаточно ли мы близко к позиции
            {                
                isMovingOnPath = true; // танк может двигаться по основному пути                  

                return isMovingOnPath; // возвращаем флаг движения по пути
            }
        }

        return isMovingOnPath; // возвращаем флаг движения по пути
    }    
 

    // Метод поворота башни
    private void LookToTarget(Transform target, Transform spectator)
    {
        Vector3 direction = (target.position - spectator.position).normalized; // направление, в котором нужно смотреть на цель
      
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z)); // получение нужного поворота к позиции 

        spectator.rotation = Quaternion.Lerp(spectator.rotation, lookRotation, Time.deltaTime * turnSpeedTower); // поворот башни к цели 
        
    }

    // Метод уничтожения и начисление очков 
    private void DestroyAndUpScore()
    {
        p_Tank.GetComponent<Collider>().enabled = false; // выключаем коллайдер, чтобы после смерти не наносился урон

        scoreCounter.UpdateScore(scoreForDeath); // передаем количество очков, которые нужно прибавить за уничтожение         

        camShake.Shake();

        DropItemChance();

        int award = (int)scoreForDeath / 2 + 10;

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + award);

        Destroy(gameObject, waitTimeDeath); // уничтожение танка              
        
    }    


    // Метод спавна стартовой позиции
    private GameObject SpawnStartWayPoint()
    {
        // высчитываем позицию для спавна стартовой позиции
        Vector3 posForSpawn = new Vector3(p_Tank.transform.position.x + offsetX, p_Tank.transform.position.y, p_Tank.transform.position.z + offsetZ);

        GameObject wayPoint = Instantiate(p_WayPoint, posForSpawn, p_Tank.transform.rotation) as GameObject; // спавним стартовую позицию в нужной точке с изначальным вращением точки

        return wayPoint; // вернуть стартовую позицию
    }

    // Метод движения по пути
    private void MoveOnWayPoints()
    {
        if (isMovingOnPath) // Если танк движется по пути
        {
            float dist = (p_Tank.transform.position - wayPoints[currentWayPoint].transform.position).sqrMagnitude; // дистанция до следующего вэйпоинта              

            Vector3 direction = wayPoints[currentWayPoint].transform.position - p_Tank.transform.position; // Направление к следующей точки в пути

            if (Vector3.Distance(wayPoints[currentWayPoint].transform.position, p_Tank.transform.position) < wp_Radius) // Если дистанция до следующей точки в пути меньше минимальной
            {
                currentWayPoint++;

                if (currentWayPoint >= wayPoints.Length) // Если доехал до последний точки пути
                {
                    currentWayPoint = Random.Range(0, wayPoints.Length); // Выбрать случайную точку из пути
                }               
            }

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // получение нужного поворота к позиции  

            p_Tank.transform.rotation = Quaternion.Lerp(p_Tank.transform.rotation, lookRotation, Time.fixedDeltaTime * turnSpeed); // поворот  
            
            // движение танка к следующей позиции
            p_Tank.transform.position = Vector3.MoveTowards(p_Tank.transform.position, wayPoints[currentWayPoint].transform.position, Time.fixedDeltaTime * moveSpeed);
        }
    }
    
    
    // Метод просчитывающий шанс выпадения предмета
    private int DropItemChance()
    {
       int chance = Random.Range(0, 100);   
       
       if (chance > dropChance && scoreCounter.ChanceDropItem() == 1)
       {
            dropLootComponent.DropItems();

            return chance;
       }        

        return chance;
    }
}
