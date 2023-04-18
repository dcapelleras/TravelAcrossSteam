using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//para elegir acciones, que haya iconos de flecha que puedes arrastrar a los limitados huecos para conseguir el puzzle con x acciones
public class CodeBlock : MonoBehaviour
{
    [SerializeField] BlockManager blockManager;
    public MovementAction action;
    public bool isSource;
    [SerializeField] GameObject objectInstantiation;
    [SerializeField] Transform spotsParent;
    List<CodeSpot> spots= new List<CodeSpot>();

    [SerializeField] int numberOfSpots;

    private void Awake()
    {
        if (blockManager == null)
        {
            blockManager = FindObjectOfType<BlockManager>();
        }
        if (isSource)
        {
            for (int i = 0; i < spotsParent.childCount; i++)
            {
                spots.Add(spotsParent.GetChild(i).GetComponent<CodeSpot>());
            }
        }
        else
        {
            for (int i = 0; i < numberOfSpots; i++)
            {
                spotsParent = GameObject.Find("placesToSpawn").transform;
                spots.Add(spotsParent.GetChild(i).GetComponent<CodeSpot>());
            }
        }
    }

    void AddToList()
    {
        blockManager.AddNewAction(action);
    }

    void RemoveFromList()
    {
        blockManager.RemoveAction(action);
    }

    private void OnMouseDown()
    {
        if (isSource) //needs objectInstantiation
        {
            for (int i = 0; i < spots.Count; i++)
            {
                if (!spots[i].isFull)
                {
                    spots[i].isFull = true;
                    Instantiate(objectInstantiation, spots[i].transform);
                    AddToList();
                    return;
                }
            }
            Debug.Log("No places available");
        }
        else //doesnt need objectInstantiation
        {
            RemoveFromList();
            for (int i = 0; i < spots.Count; i++)
            {
                spots[i].CheckIfFull();
            }
            Destroy(gameObject);
        }
    }
}
