using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Transform canvasTransform;
    [SerializeField] Transform autoLayoutPanel;

    void Start()
    {
        autoLayoutPanel = GameObject.Find("Content").transform;
        canvasTransform = GameObject.Find("CanvasInventory").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //set as a picked object (and maybe change parent so it stops autoarranging)
        transform.SetParent(canvasTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += eventData.delta / canvasTransform.GetComponent<Canvas>().scaleFactor;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //if left on top of a slot, check that item in that slot (maybe with a raycast? Or OnMouseUp in the slot)
        //  if that slot matches, delete the item from inventory
        //  if it doesn't match, bring back to the original place (changing the parent)
        
        transform.SetParent(autoLayoutPanel);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) )
        {
            if (hit.transform.TryGetComponent(out PuzzlePiece piece))
            {
                InventoryItemController item = GetComponent<InventoryItemController>();
                piece.TryPiece(item.item);
            }
        }
    }
}
