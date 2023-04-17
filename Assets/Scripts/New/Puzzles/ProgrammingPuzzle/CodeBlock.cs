using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//para elegir acciones, que haya iconos de flecha que puedes arrastrar a los limitados huecos para conseguir el puzzle con x acciones
public class CodeBlock : MonoBehaviour
{
    [SerializeField] BlockManager blockManager;
    public MovementAction action;

    public void AddToList()
    {
        blockManager.actions.Add(action);
    }
}
