using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialsManager : MonoBehaviour
{
    public int vialsRequired = 2;
    public List<Vial> vials;

    public void CheckVials()
    {
        int correctAmount = 0;
        for (int i = 0; i < vials.Count; i++)
        {
            if (vials[i].isCorrect)
            {
                correctAmount++;
            }
        }
        if (correctAmount >= vialsRequired)
        {
            Debug.Log("Congratulations, all vials are correctly sorted out");
        }
    }
}
