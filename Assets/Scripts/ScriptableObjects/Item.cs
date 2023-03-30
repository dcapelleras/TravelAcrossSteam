using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new_item", menuName = "ScriptableObjects/Create new item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public Item thisItem;
    public int value;

    public void SelectThisItem() //con boton se activa, la funcion guarda el boton clicado
    {
        Player_Interact player = FindObjectOfType<Player_Interact>();
        player.selectedItem = thisItem;
    }
}
