using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CodingManager : MonoBehaviour
{
    public List<CodingLine> codingLines;

    public List<Action> actions;

    Vector3 initialSpritePos;
    Quaternion initialSpriteRot;
    [SerializeField] Transform spriteTransform;
    [SerializeField] List<Action> actionsRequired;

    [SerializeField] float spriteMoveDistance;

    int correctCounter;

    private void Awake()
    {
        for (int i = 0; i < codingLines.Count; i++)
        {
            actions.Add(Action.none);
        }
        initialSpritePos = spriteTransform.position;
        initialSpriteRot = spriteTransform.rotation;
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
        spriteTransform.position = initialSpritePos;
        spriteTransform.rotation = initialSpriteRot;
        for (int i = 0; i < actions.Count; i++)
        {
            Debug.Log(actions[i].ToString());
            if (actions[i] == actionsRequired[i])
            {
                correctCounter++;
                if (correctCounter == actionsRequired.Count)
                {
                    //se pueden hacer cosas extra en plan cambiar el color o activar luces o cosas
                    Debug.Log("Congratulations!");
                    SceneManager.UnloadSceneAsync("CodingMinigame");
                    //maybe go to the next puzzle, maybe make only 1
                    NasaDialogueManager.instance.FinishCodingPuzzle();
                }
                if (actions[i] == Action.forward)
                {
                    spriteTransform.position += spriteTransform.up * spriteMoveDistance;
                }
                else if (actions[i] == Action.left)
                {
                    spriteTransform.Rotate(Vector3.forward * 90);
                }
                else if (actions[i] == Action.right)
                {
                    spriteTransform.Rotate(Vector3.forward * -90);
                }
            }
            else
            {
                Debug.Log("Parece que no es la combinación correcta");
                spriteTransform.position = initialSpritePos;
                spriteTransform.rotation = initialSpriteRot;
                correctCounter = 0;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
