using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeSpot : MonoBehaviour
{
    public bool isFull;

    public void CheckIfFull()
    {
        StartCoroutine(CheckFullness());
    }

    IEnumerator CheckFullness()
    {
        Debug.Log("CheckingFullness");
        yield return new WaitForSeconds(0.3f);
        if (transform.childCount > 0)
        {
            isFull = true;
        }
        else
        {
            isFull = false;
        }
    }
}
