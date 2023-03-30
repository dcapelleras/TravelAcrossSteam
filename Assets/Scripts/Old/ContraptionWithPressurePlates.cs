using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContraptionWithPressurePlates : MonoBehaviour
{
    [SerializeField] List<PressablePlate> pressablePlates = new List<PressablePlate>();

    bool success;
    public void CheckActivation()
    {
        int amountOfPlates = pressablePlates.Count;
        for (int i = 0; i < pressablePlates.Count; i++)
        {
            if (pressablePlates[i].activated)
            {
                amountOfPlates--;
            }
            if (amountOfPlates >= 0)
            {
                success = true;
            }
        }
    }
}
