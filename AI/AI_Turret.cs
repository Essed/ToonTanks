using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Turret : MonoBehaviour
{    
    public float healthPoint; // очки здоровья

    [SerializeField]
    private float maxHealthPoint; // максимальные очки здоровья
    [SerializeField]
    private float scoreForDeath; // очки за смерть
    [SerializeField]
    private float dropChance; // шанс выпадения предмета
    [SerializeField]
    private float waitTimeDeath; // задержка времени перед уничтожением при смерти  
    [SerializeField]
    private float turnSpeed; // скорость поворота башни
    [SerializeField]
    private float radius; // радиус атаки  
    [SerializeField]
    private float timePeriod; // период времени для подсчета скорострельности
    [SerializeField]
    private float Rpt; // обороты
    private float secondBetweenShot; // время между выстрелами
    private float nextTimeShot; // время до следующего выстрела


    [SerializeField]
    private Transform target; // цель на которую нужно смотреть
    [SerializeField]
    private Transform gunTower; // пушка
    [SerializeField]
    private Transform[] spawnPointsBullet; // место спавна снарядов
    [SerializeField]
    private Transform rayPoint; // место откуда будет пускаться луч   
    private DropLoot dropLootComponent; // ссылка на компонент DropLoot


    // Камера шейкер
    [SerializeField]
    private float duration;
    [SerializeField]
    private float magnitude;
    private CameraShake camShake; // ссылка на компонент CameraShake камеры

    [SerializeField]
    private GameObject bullet; // префаб снаряда
    private ScoreCount scoreCounter; // ссылка на счетчик очков    

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>(); // найти цель
        scoreCounter = GameObject.Find("Score Counter").GetComponent<ScoreCount>(); // получить счетчик очков
        dropLootComponent = GetComponent<DropLoot>(); // подключить ссылку на компонент RigidBody 
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        healthPoint = maxHealthPoint;
    }

    private void Update()
    {
        secondBetweenShot = timePeriod / Rpt; // высчитываем время между выстрелами

        if (healthPoint <= 0) // Если очки здоровья кончились
        {
            DestroyAndUpScore(); // уничтожить турель и начислить очки
        }
    }

    private void FixedUpdate()
    {
        if (healthPoint > 0) // Если турель жива
        {          

            if (target != null && CalculateDistance(target.position) < radius) // Если дистанция меньше или равна радиусу действия пушки
            {
                LookToTarget(target); // повернуться к цели
                Attack(); // атаковать
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>()) // Если соприкоснулись со снарядом 
        {
            TakeDamage(other.GetComponent<Bullet>().damage); // получить урон
            Destroy(other.gameObject);
        }
    }


    // Метод поворота к цели
    private void LookToTarget(Transform targetLook)
    {
        targetLook = target; // цель, к которой нужно повернуть

        Vector3 direction = (targetLook.position - transform.position).normalized; // нормализованное направление до цели

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // получение нужного поворота до цели

        gunTower.rotation = Quaternion.Lerp(gunTower.rotation, lookRotation, Time.fixedDeltaTime * turnSpeed); // поворот к цели
    }

    // Метод атаки цели
    private void Attack()
    {
        if (ShootDelay()) // Если можно стрелять
        {
            RaycastHit hit; // создаем луч

            if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, radius)) // Если луч попал на твердое тело
            {
                if (hit.transform.GetComponent<PlayerController>()) // Если луч попал в цель
                {
                    for (int i = 0; i < spawnPointsBullet.Length; i++) // прогоняем цикл по всем местам спавна снаяродв
                    {
                         Instantiate(bullet, spawnPointsBullet[i].position, spawnPointsBullet[i].rotation); // спавним снаряды на местах спавна 
                    }
                }
            }
            nextTimeShot = Time.time + secondBetweenShot; // увеличиваем время до следующего выстрела
        }
    }

    // Метод получения урона 
    private void TakeDamage(float damage)
    {
        healthPoint = healthPoint - damage;
    }

    // Метод уничтожения и начисление очков 
    private void DestroyAndUpScore()
    {  
        GetComponent<Collider>().enabled = false; // выключаем коллайдер, чтобы после смерти не наносился урон

        scoreCounter.UpdateScore(scoreForDeath); // передаем количество очков, которые нужно прибавить за уничтожение        

        camShake.Shake();

        DropItemChance();

        int award = (int)scoreForDeath / 2 + 10;

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + award);

        Destroy(gameObject, waitTimeDeath); // уничтожение турели    

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

    // Метод определения дистанции до цели  
    private float CalculateDistance(Vector3 targetDistance)
    {
        float distance = Vector3.Distance(transform.position,targetDistance); // вычисляет дистанцию между турелью и целью

        return distance; // возвращает значение дистанции
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

}
