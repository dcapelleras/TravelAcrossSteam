using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Create new liquid part")]
public class LiquidPart : ScriptableObject
{
    public GameObject _obj; // El color de este líquido

    public int _typeIndex;
}
