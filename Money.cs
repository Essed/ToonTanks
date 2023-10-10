using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Money", fileName = "New Money")]
public class Money : ScriptableObject
{
    [SerializeField] private int money;

    public int _Money
    {
        get { return money; }
        set { money = value; }
    }

}
