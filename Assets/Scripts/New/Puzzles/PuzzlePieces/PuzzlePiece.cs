using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] GameObject correctObject;
    public Item correctItem;
    [SerializeField] PuzzleManager puzzle;

    public void TryPiece(Item item)
    {
        if (item == correctItem)
        {
            InventoryManager_v2.instance.Remove(item);
            InventoryManager_v2.instance.ListItems();
            Instantiate(correctObject, transform.position, transform.rotation);
            puzzle.CheckParts();
            gameObject.SetActive(false);
        }
    }
}
