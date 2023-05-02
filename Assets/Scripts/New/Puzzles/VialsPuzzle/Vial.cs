using System.Collections.Generic;
using UnityEngine;

public class Vial : MonoBehaviour
{
    public int positionsAmount; //el numero de huecos para distintos liquidos

    public List<LiquidPart> liquidParts= new List<LiquidPart>();

    public int filledParts;

    public List<Transform> liquidPositions = new List<Transform>();

    public List<GameObject> liquidObjects= new List<GameObject>();

    public bool isCorrect;

    [SerializeField] VialsManager vialManager;

    private void Start()
    {
        for (int i = 0; i < liquidParts.Count; i++)
        {
            liquidObjects.Add(liquidParts[i]._obj);
        }
        UpdateVisual();
    }

    public void ExportLiquid(Vial vial)
    {
        if (vial.filledParts >= vial.positionsAmount) //si no hay huecos libres en el otro vial, no hacer nada
        {
            Debug.Log("No spots in the other vial");
            return;
        }
        else if (liquidParts.Count <= 0) //si no hay liquido en este vial
        {
            Debug.Log("No liquid in this vial");
            return;
        }
        vial.ImportLiquid(liquidParts[liquidParts.Count-1]); //añadir el primer liquido al otro vial
        liquidParts.RemoveAt(liquidParts.Count-1); //borrar primer liquido de este vial
        filledParts--;
        UpdateVisual();
    }

    public void ImportLiquid(LiquidPart liquid)
    {
        filledParts++;
        liquidParts.Add(liquid);
        UpdateVisual();
        if (filledParts == liquidPositions.Count) 
        {
            int type = -1;
            for (int i = 0; i < liquidParts.Count; i++) //por cada liquidpart
            {
                if (type < 0)
                {
                    type = liquidParts[i]._typeIndex; //guarda el primer tipo de las parts
                }
                else
                {
                    if (liquidParts[i]._typeIndex != type) //a partir del segundo, si no es igual que el primero, se queda incorrect
                    {
                        isCorrect = false;
                        return;
                    }
                }
            }
            isCorrect = true;
            vialManager.CheckVials();
        }

    }

    public void UpdateVisual()
    {
        for (int i = 0; i < liquidObjects.Count; i++)
        {
            if (liquidObjects[i].activeInHierarchy)
            {
                liquidObjects[i].SetActive(false);
            }
        }
        liquidObjects.Clear();
        for (int i = 0; i < filledParts; i++)
        {
            GameObject newObject = Instantiate(liquidParts[i]._obj, liquidPositions[i]);
            liquidObjects.Add(newObject);
        }
    }
}
