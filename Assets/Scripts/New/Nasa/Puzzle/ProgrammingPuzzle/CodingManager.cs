using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodingManager : MonoBehaviour
{
    [SerializeField] int puzzleIndex;

    public List<CodingLine> codingLines;

    public List<Action> actions;

    Vector3 initialSpritePos;
    Quaternion initialSpriteRot;
    [SerializeField] Transform spriteTransform;
    Rigidbody2D spriteRb;
    [SerializeField] List<Action> actionsRequired;
    

    [SerializeField] float spriteMoveDistance;

    [SerializeField] float timeBetweenMoves = 1f;

    public bool hasCollided;

    int correctCounter;

    private void Awake()
    {
        spriteRb = spriteTransform.GetComponent<Rigidbody2D>();
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
        hasCollided = false;
        spriteRb.position = initialSpritePos;
        spriteTransform.rotation = initialSpriteRot;
        for (int i = 0; i < actions.Count; i++)
        {
            Debug.Log(actions[i].ToString());
            if (actions[i] == Action.forward)
            {
                spriteRb.MovePosition(spriteRb.position + (Vector2)spriteTransform.up * spriteMoveDistance);
            }
            else if (actions[i] == Action.left)
            {
                spriteTransform.Rotate(Vector3.forward * 90);
            }
            else if (actions[i] == Action.right)
            {
                spriteTransform.Rotate(Vector3.forward * -90);
            }

            if (actions[i] == actionsRequired[i])
            {
                correctCounter++;
                if (correctCounter == actionsRequired.Count)
                {
                    switch (puzzleIndex)
                    {
                        case 0:
                            SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);
                            SceneManager.UnloadSceneAsync(4);
                            break;
                        case 1:
                            SceneManager.LoadSceneAsync(6, LoadSceneMode.Additive);
                            SceneManager.UnloadSceneAsync(5);
                            break;
                        case 2:
                            SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);
                            SceneManager.UnloadSceneAsync(6);
                            break;
                        case 3:
                            SceneManager.LoadSceneAsync(8, LoadSceneMode.Additive);
                            SceneManager.UnloadSceneAsync(7);
                            break;
                        case 4:
                            NasaDialogueManager.instance.FinishCodingPuzzle();
                            SceneManager.UnloadSceneAsync(8);
                            break;
                    }
                    
                }
            }
            float timeToWait = 0;
            if (actions[i] != Action.none)
            {
                timeToWait = timeBetweenMoves;
            }
            yield return new WaitForSeconds(timeToWait);
            if (hasCollided)
            {
                WrongCombination();
                yield break;
            }
            
        }
        spriteRb.position = initialSpritePos;
        spriteTransform.rotation = initialSpriteRot;

    }

    void WrongCombination()
    {
        Debug.Log("Parece que no es la combinación correcta");
        spriteTransform.position = initialSpritePos;
        spriteTransform.rotation = initialSpriteRot;
        correctCounter = 0;
    }
}
