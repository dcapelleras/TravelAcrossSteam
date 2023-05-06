using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPuzzle : MonoBehaviour
{
    public List<string> PuzzleNames = new List<string>();
    public bool finished = false;
    public List<PuzzlePart> parts = new List<PuzzlePart>();

    public void CheckParts() //comprueba si todas las parts estan correctas
    {
        for (int i = 0; i < parts.Count; i++)
        {
            if (!parts[i].partCorrect)
            {
                finished = false;
                return;
            }
        }
        Debug.Log("All parts succesfully mounted");
        finished = true;
    }


}
