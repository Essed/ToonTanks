using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private float waveCount;
    [SerializeField]
    private float currentWave;

    [SerializeField]
    private float maxDelayWaves;
    [SerializeField]
    private float currentWaveTime;


    [SerializeField]
    private GameObject[] spawnersActive;
    [SerializeField]
    private GameObject[] turrets;

    [SerializeField]
    private GameObject[] doorsForOpen;

    private bool trigger;




    private void Start()
    {
        currentWave = waveCount;
        
    }

    private void Update()
    {
        currentWaveTime++;

        if (currentWaveTime == maxDelayWaves)
        {
            for (int i = 0; i < spawnersActive.Length; i++)
            {
                if (spawnersActive[i].GetComponent<Spawner>().enabled == false)
                {
                    currentWaveTime = 0;
                }
            }
        }

        if (currentWave > 0)
        {
            if (currentWaveTime >= maxDelayWaves)
            {
                for (int i = 0; i < spawnersActive.Length; i++)
                {
                    if (spawnersActive[i].GetComponent<Spawner>().currentEnemyCount == spawnersActive[i].GetComponent<Spawner>().maxCountSpawn)
                    {
                        currentWave--;
                        currentWaveTime = 0;
                        spawnersActive[i].GetComponent<Spawner>().currentEnemyCount = 0;

                    }
                }
            }
        }

        if(currentWave < 0)
        {          
            currentWave = 0;
            currentWaveTime = 0;                                  
        }

        if(currentWave == 0)
        {
            int count = 0;

            foreach (var item in turrets)
            {
                if(item == null)
                {
                    count++;
                }
            }

            if (count == turrets.Length)
            {
                OpenNextArea();
            }
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            for(int i = 0; i < spawnersActive.Length; i++)
            {
                spawnersActive[i].GetComponent<Spawner>().enabled = true;
                spawnersActive[i].GetComponent<Spawner>().currentEnemyCount = 0;               
            }

            gameObject.GetComponent<Collider>().enabled = false;
        }
    }


    private void OpenNextArea()
    {
        for (int i = 0; i < doorsForOpen.Length; i++)
        {
            doorsForOpen[i].transform.position = new Vector3(doorsForOpen[i].transform.position.x, 3.12f, doorsForOpen[i].transform.position.z);
        }
    }


}

