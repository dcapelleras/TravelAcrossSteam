using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turner : MonoBehaviour
{
    [SerializeField] int currentRotation = 0;


    private void OnMouseDown()
    {
        if (currentRotation == 0)
        {
            currentRotation = 1;
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            currentRotation = 0;
        }
    }
}
