using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePart : MonoBehaviour
{
    public Item objectReq;
    public bool partCorrect = false;
    public DemoPuzzle puzzle;
    public Inventory inventory;
    public Material correctMat;


    public void CheckObject(Item item) //se llama desde player, comprueba si el objeto que esta sujetando el jugador es el indicado para esta Part
    {
        if (item == objectReq)
        {
            partCorrect = true;
            puzzle.CheckParts();
            Debug.Log(item.name + "Has been accepted");
            inventory.RemoveItem(item);
            GetComponent<MeshRenderer>().material = correctMat;
            InventoryManager.instance.UpdateInventory();
        }
    }
}
