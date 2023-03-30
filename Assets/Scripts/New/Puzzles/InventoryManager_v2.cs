using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager_v2 : MonoBehaviour
{
    public Inventory_v2 inventory;
    public static InventoryManager_v2 instance;
    [SerializeField] GameObject InventoryPanel;
    public Transform itemContent;
    public GameObject inventoryItem;
    public List<Item> _items = new List<Item>();

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        inventory.items.Clear(); //cancels saving
        if (inventory.items.Count > 0 ) //for saving
        {
            foreach (Item item in inventory.items)
            {
                _items.Add(item);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        if (InventoryPanel.activeInHierarchy)
        {
            InventoryPanel.SetActive(false);
            return;
        }
        InventoryPanel.SetActive(true);
        ListItems();
    }

    public void Add(Item item)
    {
        inventory.items.Add(item);
        _items.Add(item);
    }

    public void Remove(Item item)
    {
        inventory.items.Remove(item);
        _items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in _items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            obj.GetComponent<InventoryItemController>().item = item;
            var itemName = obj.transform.Find("itemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("icon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite= item.icon;
        }
        
    }
}
