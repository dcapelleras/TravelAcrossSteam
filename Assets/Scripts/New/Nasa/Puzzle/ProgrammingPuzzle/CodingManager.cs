using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CodingManager : MonoBehaviour
{
    public List<CodingLine> codingLines;

    public List<Action> actions;

    private void Awake()
    {
        for (int i = 0; i < codingLines.Count; i++)
        {
            actions.Add(Action.none);
        }
    }

    public void UpdateCodeList(int lineIndex, Action _action)
    {
        actions[lineIndex] = _action;
    }

    public void ExecuteActions()
    {
        StartCoroutine(RunCodingCommands());
    }

    IEnumerator RunCodingCommands()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            Debug.Log(actions[i].ToString());
            yield return new WaitForSeconds(1);
        }
    }
}
