using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour //item icon in inventory
{
    public Item item;
    private void Start()
    {

       // Button btn = GetComponent<Button>();
       // btn.onClick.AddListener(SelectItem);
    }

    public void SelectItem() //not being called
    {
        Debug.Log("Selecting item");
        Player_Interact player = FindObjectOfType<Player_Interact>();
        player.selectedItem = item;
    }
}
