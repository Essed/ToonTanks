using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellActivator : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();  
    }


    public void ActivateSpeell()
    {
        if (!player.isSpelling && player.currentScaleSpell == player.scaleSpell)
        {
            player.isSpelling = true;
        }
        else
        {
            player.isSpelling = false;
        }
        
    }
}
