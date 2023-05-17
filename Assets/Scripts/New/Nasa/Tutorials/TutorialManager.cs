using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public List<TutorialScriptable> tutorials= new List<TutorialScriptable>();

    public TutorialScriptable currentTutorial;

    [SerializeField] TMP_Text tutorialTextArea;

    [SerializeField] GameObject tutorialObject;

    public bool shouldStartWithTutorial;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (shouldStartWithTutorial)
        {
            tutorialObject.SetActive(true);
            ShowTutorial(0);
        }
    }

    public void ShowTutorial(int tutorialIndex)
    {
        tutorialObject.SetActive(true);
        tutorialTextArea.text = tutorials[tutorialIndex].tutorialText;
    }
}
