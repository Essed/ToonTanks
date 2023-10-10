using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void Update()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        transform.LookAt(target);
    }
}
