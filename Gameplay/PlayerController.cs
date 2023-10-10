using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float healthPoint; // очки здоровья
    [SerializeField]
    private float maxHealthPoint; // максимально возможное здоровье
    
    // Эффекты
    private Items itemEffect; // эффект  
    [SerializeField]
    private float effectActionTime; // текущее время действия эффектов 
    private float maxActionTime; // текущее время действия эффектов   
    private bool affectFlag; // флаг действия эффекта

    [SerializeField]
    private Bullet p_Bullet; // префаб пули для контроля действия эффекта PowerUp
    [SerializeField]
    private float damage; // Эталон урона
    private float baseDamage; // базовый урон снаряда для контроля
    private float deltaDamage; // изменение урона

    [SerializeField]
    private ScoreCount scoreCounter; // ссылка на счетчик очков

    // Способности
    [SerializeField]
    private GameObject p_Spell; // префаб способности для спавна
    [SerializeField]
    private Transform spellSpot; // префаб способности для спавна
    [HideInInspector]
    public float scaleSpell; // шкала зарядка способности
    [HideInInspector]
    public float currentScaleSpell; // шкала зарядка способности
    public bool isSpelling; // флаг активации способности


    // Элементы худа
    [SerializeField]
    private HealthBar hpBar; // ссылка на компонент HealthBar
    [SerializeField]
    private SpellBar spBar; // ссылка на компонент SpellBar
    [SerializeField]
    private GameObject effectBar; // ссылка на компонент SpellBar
    [SerializeField]
    private Button activateSpellButton; // кнопка активации способности

    private void Start()
    {
        currentScaleSpell = 0;
        p_Bullet.damage = damage;
        baseDamage = p_Bullet.damage;   
        deltaDamage = baseDamage;
        scoreCounter = GameObject.Find("Score Counter").GetComponent<ScoreCount>();
        healthPoint = maxHealthPoint;        
    }

    private void Update()
    {
        currentScaleSpell++;

        UseEffect();        

        if (hpBar != null)
        {
            float hp = (healthPoint / maxHealthPoint);           
            hpBar.FillBar(hp);
        }

        if (spBar != null)
        {
            float curScaleSpell = (currentScaleSpell / scaleSpell);
            spBar.FillBar(curScaleSpell);
        } 

        if (effectBar != null && maxActionTime != 0)
        {
           effectBar.SetActive(true);
           float effectPoints = (effectActionTime / maxActionTime);
           effectBar.transform.GetChild(0).GetComponent<EffectBar>().FillBar(effectPoints);
           
            if(effectBar.transform.GetChild(0).GetComponent<EffectBar>().FillBar(effectPoints) == 0)
            {
                p_Bullet.damage = damage;
                effectBar.SetActive(false);
            }
        }             
        
        if (currentScaleSpell >= scaleSpell)
        {            
            currentScaleSpell = scaleSpell;           
        }

        if (currentScaleSpell == scaleSpell)
        {
            activateSpellButton.interactable = true;

            if (isSpelling)
            {
                UseSpell();

                currentScaleSpell = 0;
                isSpelling = false;
                activateSpellButton.interactable = false;
            }
        }

        if (currentScaleSpell < scaleSpell)
        {
            activateSpellButton.interactable = false;
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AI_Bullet>())
        {
            TakeDamage(other.GetComponent<AI_Bullet>().damage);
        }

        if (other.GetComponent<Items>())
        {
            itemEffect = other.GetComponent<Items>();

            if (itemEffect.Type == Items.ItemType.DoubleScore || itemEffect.Type == Items.ItemType.PowerUp)
            {
                maxActionTime = other.GetComponent<Items>().timeAction;
                effectActionTime = other.GetComponent<Items>().timeAction;
                affectFlag = true;
                Destroy(other.gameObject);
            }

        }
    }

    // Метод получения урона 
    private void TakeDamage(float damage)
    {
        healthPoint = healthPoint - damage;
    }  

    // Метод уничтожения
    private void DeadDestroy()
    {
        Destroy(gameObject); // уничтожение танка
    }
      
    // Метод использования эффектов
    private void UseEffect()
    {
        if (healthPoint > maxHealthPoint)
        {
            healthPoint = healthPoint - (healthPoint - maxHealthPoint);
        }
       
        if (healthPoint <= 0)
        {
            SceneManager.LoadScene("Main Menu");
            DeadDestroy();
        }

        if (affectFlag == true && effectActionTime > 0)
        {
            effectActionTime--;
        }

        if (itemEffect != null)
        {
            if (effectActionTime <= 0 && itemEffect.Type == Items.ItemType.PowerUp)
            {                
                p_Bullet.damage = baseDamage;
                return;
            }

            if (effectActionTime <= 0 && itemEffect.Type == Items.ItemType.DoubleScore)
            {               
                scoreCounter.scoreFactor = 0;
                return;
            }

            if (effectActionTime > 0 && itemEffect.Type == Items.ItemType.DoubleScore && scoreCounter.scoreFactor > 0)
            {
                scoreCounter.scoreFactor = 2;
            }

            if (effectActionTime > 0 && itemEffect.Type == Items.ItemType.PowerUp && p_Bullet.damage > deltaDamage + baseDamage)
            {
                p_Bullet.damage = damage + baseDamage;
            }

        }
    }

    // Метод использования способности
    private void UseSpell()
    {
        Instantiate(p_Spell, spellSpot.position, spellSpot.rotation);
    }

}
