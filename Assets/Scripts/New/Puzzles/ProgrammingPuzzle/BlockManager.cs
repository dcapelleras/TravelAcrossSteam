using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public List<MovementAction> actions = new List<MovementAction>();

    [SerializeField] float moveAmount = 1f;
    Vector3 initialPos;
    Quaternion initialRotation;
    Coroutine executingCoroutine;
    [SerializeField] Transform spotsParent;
    List<CodeSpot> spots = new List<CodeSpot>();
    [SerializeField] float distanceToDetectWall = 1f;

    private void Awake()
    {
        initialPos= transform.position;
        initialRotation = transform.rotation;
        for (int i = 0; i < spotsParent.childCount; i++)
        {
            spots.Add(spotsParent.GetChild(i).GetComponent<CodeSpot>());
        }
    }

    public void AddNewAction(MovementAction _action)
    {
        //actions.Add(_action);
        actions.Clear();
        for (int i = 0; i < spots.Count; i++)
        {
            if (spots[i] == null)
            {
                Debug.Log("No existe");
                return;
            }
            if (spots[i].TryGetComponent(out CodeSpot spot))
            {
                if (spot.isFull)
                {
                    actions.Add(spot.GetComponentInChildren<CodeBlock>().action);
                }
            }
        }
    }

    public void RemoveAction(MovementAction _action)
    {
        actions.Remove(_action);
    }

    public void ExecuteActions()
    {
        transform.position = initialPos;
        transform.rotation = initialRotation;
        executingCoroutine = StartCoroutine(ExecuteAction());
    }

    public void StopExecution()
    {
        StopCoroutine(executingCoroutine);
        transform.position = initialPos;
        transform.rotation = initialRotation;
        //actions.Clear();
    }

    IEnumerator ExecuteAction()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].actionIndex == 0)
            {
                MoveForward();
            }
            else if (actions[i].actionIndex == 1)
            {
                TurnLeft();
            }
            else
            {
                TurnRight();
            }
            
            yield return new WaitForSeconds(1f);
        }
        //actions.Clear();
    }

    void MoveForward()
    {
        Debug.DrawRay(transform.position, transform.forward * distanceToDetectWall, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward,out hit, distanceToDetectWall))
        {
            if (hit.collider.CompareTag("Final"))
            {
                Debug.Log("Es bien");
                return;
            }
            Debug.Log("You collided with a wall");
            StopExecution();
            return;
        }
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
