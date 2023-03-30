using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Transform spritesInventory;
    public static InventoryManager instance;
    [SerializeField] Inventory inventory;

    public GameObject testObject;

    List<GameObject> _inventoryItems= new List<GameObject>();

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < inventory.inventoryItems.Count; i++) //por cada uno del inventario
        {
            if (true)//inventory.inventoryItems[i].inInventory) //si el del inventario esta en el inventario
            {
                if (_inventoryItems.Count < inventory.inventoryItems.Count)
                {
                    Debug.Log("adding new instance from prefab");
                    //GameObject newItem = Instantiate(inventory.inventoryItems[i].icon, spritesInventory);
                    //_inventoryItems.Add(newItem);
                }
            }
            else
            {
                //Destroy(_inventoryItems[i]);
            }
        }
    }
}
