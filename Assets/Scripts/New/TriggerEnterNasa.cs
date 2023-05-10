using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class TriggerEnterNasa : MonoBehaviour
{
    DialogueRunner runner;
    [SerializeField] Transform moveHereIfNotEnter;

    private void Awake()
    {
        runner = FindObjectOfType<DialogueRunner>();
        runner.AddCommandHandler("enterBuilding", GoToInsideScene);
        runner.AddCommandHandler("dontEnter", DontGoInside);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            runner.Dialogue.Stop();
            runner.StartDialogue("ShouldEnter");
        }
    }

    public void GoToInsideScene()
    {
        SceneManager.LoadScene(3);
    }

    public void DontGoInside()
    {
        NasaNavigation nav = FindObjectOfType<NasaNavigation>();
        nav.MoveToThisDestination(moveHereIfNotEnter);
    }
}
