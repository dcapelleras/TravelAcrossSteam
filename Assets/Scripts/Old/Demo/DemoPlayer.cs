using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Yarn.Unity;

public class DemoPlayer : MonoBehaviour
{
    Camera cam;

    public bool usingThisCharacter = true;

    [SerializeField] float moveSpeed = 10f;

    Vector3 goalPosition;

    public Inventory inventory;

    GameObject pickingObject;

    GameObject interactingPerson;

    Animator anim;

    DialogueRunner dialogueRunner;

    [SerializeField] List<DemoPlayer> players = new List<DemoPlayer>();

    [SerializeField] CamCinemachine camController;

    public int playerIndex = 0;
    public bool crossingDoor;

    [SerializeField] Transform door1Position;
    [SerializeField] Transform door2Position;

    public Transform spawnPosition;

    public bool doingPuzzle;

    public Item holdingObject;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        cam = Camera.main;
        goalPosition = transform.position;
        if (TryGetComponent(out Animator animator))
        {
            anim = animator;
        }
    }

    private void Update()
    {
        if (doingPuzzle) //si esta haciendo puzzle
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoPuzzle();
                return;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                doingPuzzle = false;
                camController.ZoomBackToRoom();
            }
        }
        if (!usingThisCharacter)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RayMouse();
        }

        Vector3 stopPosition = new Vector3(goalPosition.x, transform.position.y, goalPosition.z);
        if (Vector3.Distance(transform.position, stopPosition) > 0.1f)
        {
            Vector3 direction = goalPosition - transform.position;
            direction.Normalize();
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else if (crossingDoor)
        {
            pickingObject = null;
            crossingDoor = false;


            if (playerIndex == 0) //player1 pulsa puerta
            {
                players[1].transform.position = players[1].spawnPosition.position; //player 2 se va al spawn de player2
                players[1].usingThisCharacter = true; //player 2 se activa
                players[0].usingThisCharacter = false; //player 1 se desactiva
                camController.CineChange(2);
                players[1].enabled = true;
                players[0].enabled = false;
            }
            else //player2 pulsa puerta
            {
                players[0].transform.position = players[0].spawnPosition.position; //player 1 se va al spawn de player1
                players[0].usingThisCharacter = true; //player 1 se activa
                players[1].usingThisCharacter = false; // player 2 se desactiva
                camController.CineChange(1);
                players[0].enabled = true;
                players[1].enabled = false;
            }
            return;
        }
        else if (interactingPerson != null)
        {
            pickingObject = null;
            if (interactingPerson.TryGetComponent(out DemoScientist scientist))
            {

            }
        }
                pickingObject.GetComponent<DemoInteractable>().Interact();
                pickingObject = null;



        if (Vector3.Distance(transform.position, stopPosition) > 0.1f)
        {
            //anim.SetBool("isRun", true);
            if (stopPosition.x < transform.position.x)
            {
                anim.SetBool("runLeft", false);
                anim.SetBool("runRight", true);
            }
            else
            {
                anim.SetBool("runRight", false);
                anim.SetBool("runLeft", true);
            }

        }
        else
        {
            anim.SetBool("runLeft", false);
            anim.SetBool("runRight", false);
        }
    }

    void DoPuzzle()
    {
        Debug.Log("Trying puzzle with a " + holdingObject.name);
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out PuzzlePart part))
            {
                part.CheckObject(holdingObject);
            }
        }
    }

    public void SelectItem(Item item) //con boton se activa, la funcion guarda el boton clicado
    {
        if (item != null && doingPuzzle)
        {
            holdingObject = item;
        }

    }

    void RayMouse()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if (hit.transform.TryGetComponent(out DemoInteractable interactable))
                {
                    goalPosition = interactable.moveToPosition.position;
                    //maybe correct height, maybe not needed
                    pickingObject = hit.collider.gameObject;
                }
            }
            else if (hit.collider.CompareTag("Scientist"))
            {
                if (hit.collider.TryGetComponent(out DemoScientist scientist))
                {
                    goalPosition = scientist.moveToPosition.position;
                    //hight correction?
                    if (inventory.inventoryItems.Count > 0)
                    {
                        interactingPerson = scientist.gameObject;

                    }
                    else
                    {
                        Debug.Log("You don't have any items");
                    }
                }
            }
            else if (hit.collider.CompareTag("Portal"))
            {
                anim.Play("portalAnim");
                dialogueRunner.Dialogue.Stop();
                dialogueRunner.StartDialogue("MuertePorPortal");
            }
            else if (hit.collider.CompareTag("Door"))
            {
                if (playerIndex == 0)
                {
                    goalPosition = door1Position.position;
                }
                else
                {
                    goalPosition = door2Position.position;
                }
                crossingDoor = true;
                Debug.Log("Crossing door");
            }
            else if (hit.collider.CompareTag("Puzzle"))
            {
                Debug.Log("Focusing on puzzle");
                doingPuzzle= true;
                camController.ZoomOnPuzzle();
            }
        }
    }

    public void Disappear()
    {
        players[0].gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
}



