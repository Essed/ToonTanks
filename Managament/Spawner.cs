using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float maxDelay;
    public int maxCountSpawn;
    public int currentEnemyCount;

    [SerializeField]
    private float timeToSpawn;
    [SerializeField]
    private GameObject[] p_Enemy;     
    
   

    [SerializeField]
    private Transform spawnPoint;    

    private void Start()
    {
        currentEnemyCount = 0;
      
    }

    private void Update()
    {
        if (timeToSpawn < maxDelay && currentEnemyCount < maxCountSpawn)
        {
            timeToSpawn++;
        }         

        Spawn();      
    }


    private void Spawn()
    {
        if (currentEnemyCount < maxCountSpawn && timeToSpawn == maxDelay)
            {
                int indexEnemySpawn = Random.Range(0, p_Enemy.Length);

                 for (int i = 0; i < p_Enemy.Length; i++)
                 {                    

                    Instantiate(p_Enemy[i], spawnPoint.position, spawnPoint.rotation);
                 }

                currentEnemyCount++;     

                timeToSpawn = 0;
            }     
        
    }
}
