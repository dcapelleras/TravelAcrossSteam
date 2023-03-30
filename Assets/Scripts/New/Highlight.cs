using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] Material normalMat;
    [SerializeField] Material highlightMat;

    private void OnMouseEnter()
    {
        transform.GetComponent<Renderer>().material = highlightMat;
    }

    private void OnMouseExit()
    {

        transform.GetComponent<Renderer>().material = normalMat;
    }
}

