using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private Transform dropSpot;


    // Метод выпадения предметов 
    public void DropItems()
    {
        float indexItem = Random.Range(0, items.Length);        

        for(int i = 0; i < items.Length; i++)
        {
            if (i == indexItem)
            {
                Instantiate(items[i],dropSpot.position,dropSpot.rotation);
            }
        }        
    }  
}
