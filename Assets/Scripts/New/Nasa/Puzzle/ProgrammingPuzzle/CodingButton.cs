using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodingButton : MonoBehaviour
{
    public int actionIndex;
    [SerializeField] CodingLine line;
    [SerializeField] List<CodingButton> buttonsInSameLine;
    Image image;
    [SerializeField] Color selectedColor;
    [SerializeField] Color notSelectedColor;

    private void Awake()
    {
        image= GetComponent<Image>();
    }

    public void SelectThisAction()
    {
        image.color= selectedColor;
        for (int i = 0; i < buttonsInSameLine.Count; i++)
        {
            buttonsInSameLine[i].GetComponent<Image>().color = notSelectedColor;
        }
        line.MarkSpot(actionIndex);
    }
}
