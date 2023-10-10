using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedArea : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    [SerializeField]
    private GameObject waveManager; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            
            waveManager.GetComponent<WaveManager>().enabled = false;
            door.transform.position = new Vector3(door.transform.position.x, 1.27f, door.transform.position.z);
            
        }
    }   
}
