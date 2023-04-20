using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Create new liquid part")]
public class LiquidPart : ScriptableObject
{
    public GameObject _obj; // El color de este l�quido

    public int _typeIndex;
}
