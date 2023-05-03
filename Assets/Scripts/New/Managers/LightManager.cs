using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] List<Light> warehouselights;
    [SerializeField] float warehouseIntensity = 4f;

    public static LightManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void TurnOnWarehouse()
    {
        foreach (Light l in warehouselights)
        {
            l.intensity = warehouseIntensity;
        }
    }
}