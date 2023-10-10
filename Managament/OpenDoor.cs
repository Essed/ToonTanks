using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {          
            door.transform.position = new Vector3(door.transform.position.x, 3.12f, door.transform.position.z);
        }
    }
}
