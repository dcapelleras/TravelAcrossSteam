using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_inventory", menuName = "ScriptableObjects/Create new inventory")]
public class Inventory : ScriptableObject //base para crear inventarios que se pueden compartir entre mas de un objeto
{
    public List<Item> inventoryItems = new List<Item>();

    public void AddNewItem(Item item)
    {
        
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
            //item.inInventory = true;
            //item.inInventory = true;
            InventoryManager.instance.UpdateInventory();
        }
    }

    public void RemoveItem(Item item)
    {
        //item.inInventory= false;
        InventoryManager.instance.UpdateInventory();
        inventoryItems.Remove(item);
    }

}
