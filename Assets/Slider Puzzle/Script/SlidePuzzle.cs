using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzle : MonoBehaviour
{
    public bool slideActive = true;

    public delegate void OnComplete();
    public OnComplete onComplete;

    public int roll = 3;
    public int column = 3;

    private int[][] boardArray;

    public GameObject[] slideObject;

    [HideInInspector]
    public bool canSlide = true;

    public bool shakeCamera = true;
    private Vector3 saveRotation;

    public AudioSource audioSource;
    public AudioClip dragSound;

    public ParticleSystem particle;

    void Start ()
    {
        boardArray = new int[roll][];

        Setup();

        if (shakeCamera)
        {
            saveRotation = Camera.main.transform.rotation.eulerAngles;
        }
    }

    public void Setup ()
    {
        int score = 0;
        for (int i = 0; i < (roll * column) - 1; i++)
        {
            int y = Mathf.FloorToInt(i / roll);
            int x = i - (y * roll);

            if (boardArray[y] == null)
            {
                boardArray[y] = new int[column];
            }

            boardArray[y][x] = score;
            score++;
        }
        boardArray[roll - 1][column - 1] = -1;

        int emplyIndex = (roll * column) - 1;
        int num = 0;

        while (num != (roll * column) * 10)
        {
            bool complete = false;

            while (!complete)
            {
                int y = Mathf.FloorToInt(emplyIndex / roll);
                int x = emplyIndex - (y * roll);

                int rand = Random.Range(0, 4);
                if (rand == 0 && checkNotEmplySlot(y, x - 1))
                {
                    boardArray[y][x] = boardArray[y][x - 1];
                    boardArray[y][x - 1] = -1;
                    emplyIndex = (y * roll) + (x - 1);
                    complete = true;
                }
                if (rand == 1 && checkNotEmplySlot(y, x + 1))
                {
                    boardArray[y][x] = boardArray[y][x + 1];
                    boardArray[y][x + 1] = -1;
                    emplyIndex = (y * roll) + (x + 1);
                    complete = true;
                }
                if (rand == 2 && checkNotEmplySlot(y - 1, x))
                {
                    boardArray[y][x] = boardArray[y - 1][x];
                    boardArray[y - 1][x] = -1;
                    emplyIndex = ((y - 1) * roll) + x;
                    complete = true;
                }
                if (rand == 3 && checkNotEmplySlot(y + 1, x))
                {
                    boardArray[y][x] = boardArray[y + 1][x];
                    boardArray[y + 1][x] = -1;
                    emplyIndex = ((y + 1) * roll) + x;
                    complete = true;
                }
            }

            num++;
        }

        for (int y = 0; y < boardArray.Length; y++)
        {
            for (int x = 0; x < boardArray[y].Length; x++)
            {
                int index = boardArray[y][x];
                if (index != -1)
                {
                    Vector3 pos = new Vector3(0, slideObject[index].transform.localPosition.y, 0);

                    pos.x = (x - 1) * 1.27f;
                    pos.z = (y - 1) * -1.27f;

                    slideObject[index].transform.localPosition = pos;
                }
            }
        }
    }

    public void SlideIndex (int index)
    {
        if (canSlide && slideActive)
        {
            int y = Mathf.FloorToInt(index / roll);
            int x = index - (y * roll);

            if (boardArray[y][x] != -1)
            {
                int saveClickValue = boardArray[y][x];

                if (checkEmplySlot(y, x - 1))
                {
                    boardArray[y][x] = -1;
                    boardArray[y][x - 1] = saveClickValue;
                    moveObject(saveClickValue, y, x - 1);
                }
                if (checkEmplySlot(y, x + 1))
                {
                    boardArray[y][x] = -1;
                    boardArray[y][x + 1] = saveClickValue;
                    moveObject(saveClickValue, y, x + 1);
                }
                if (checkEmplySlot(y - 1, x))
                {
                    boardArray[y][x] = -1;
                    boardArray[y - 1][x] = saveClickValue;
                    moveObject(saveClickValue, y - 1, x);
                }
                if (checkEmplySlot(y + 1, x))
                {
                    boardArray[y][x] = -1;
                    boardArray[y + 1][x] = saveClickValue;
                    moveObject(saveClickValue, y + 1, x);
                }
            }
        }
    }

    private bool checkEmplySlot (int y, int x)
    {
        if (x >= 0 && y >= 0 && x <= column - 1 && y <= roll - 1)
        {
            if (boardArray[y][x] == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool checkNotEmplySlot(int y, int x)
    {
        if (x >= 0 && y >= 0 && x <= column - 1 && y <= roll - 1)
        {
            if (boardArray[y][x] != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private int moveIndex;
    private Vector3 moveToPosition;
    private void moveObject (int index, int y, int x)
    {
        Vector3 moveTo = new Vector3(0, slideObject[index].transform.localPosition.y, 0);
        moveTo.x = (x - 1) * 1.27f;
        moveTo.z = (y - 1) * -1.27f;

        moveIndex = index;
        moveToPosition = moveTo;
        canSlide = false;
        time = 0;

        audioSource.PlayOneShot(dragSound);
        if (particle != null)
        {
            particle.Emit(25);
        }
    }

    private bool CheckWin ()
    {
        int score = 0;
        for (int i = 0; i < (roll * column) - 1; i++)
        {
            int y = Mathf.FloorToInt(i / roll);
            int x = i - (y * roll);

            if (boardArray[y][x] == i)
            {
                score++;
            }
        }
        if (score == 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float time = 0;
	void Update ()
    {
		if (!canSlide)
        {
            time += Time.deltaTime;
            slideObject[moveIndex].transform.localPosition = Vector3.MoveTowards(slideObject[moveIndex].transform.localPosition, moveToPosition, 0.25f);

            if (shakeCamera)
            {
                Vector3 newRand = new Vector3();
                newRand.x = UnityEngine.Random.Range(-0.55f, 0.55f) * (0.8f - time) + saveRotation.x;
                newRand.y = UnityEngine.Random.Range(-0.55f, 0.55f) * (0.8f - time) + saveRotation.y;
                newRand.z = UnityEngine.Random.Range(-0.55f, 0.55f) * (0.8f - time) + saveRotation.z;

                Camera.main.transform.rotation = Quaternion.Euler(newRand);
            }

            float distance = Vector3.Distance(slideObject[moveIndex].transform.localPosition, moveToPosition);

            if (distance <= 0.02f)
            {
                time = 0;
                canSlide = true;
                if (CheckWin())
                {
                    onComplete();
                }
            }
        }
	}
}