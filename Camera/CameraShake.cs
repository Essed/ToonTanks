using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Animator camShake;
    

    public void Shake()
    {
        camShake.SetTrigger("Shake");
    }

}
