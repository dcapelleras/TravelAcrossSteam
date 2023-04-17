using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public List<MovementAction> actions = new List<MovementAction>();

    [SerializeField] float moveAmount = 1f;
    Vector3 initialPos;

    private void Awake()
    {
        initialPos= transform.position;
    }

    public void AddNewAction(MovementAction _action)
    {
        actions.Add(_action);
    }

    public void ExecuteActions()
    {
        StartCoroutine(ExecuteAction());
    }

    public void StopExecution()
    {
        StopCoroutine(ExecuteAction());
        transform.position = initialPos;
        actions.Clear();
    }

    IEnumerator ExecuteAction()
    {
        foreach (MovementAction action in actions)
        {
            switch (action.actionIndex)
            {
                case 0:
                    MoveForward();
                    break;
                case 1:
                    TurnLeft();
                    break;
                case 2:
                    TurnRight();
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        actions.Clear();
    }

    void MoveForward()
    {
        transform.position += transform.forward * moveAmount;
    }

    void TurnLeft()
    {
        transform.Rotate(0f, -90f, 0f);
    }

    void TurnRight()
    {
        transform.Rotate(0f, 90f, 0f);
    }
}
