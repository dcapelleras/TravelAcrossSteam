using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public int clickIndex = 0;
    public SlidePuzzle slidePuzzle;

    void OnMouseDown()
    {
        slidePuzzle.SlideIndex(clickIndex);
    }
}
