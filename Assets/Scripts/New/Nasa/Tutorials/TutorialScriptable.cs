using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="tutorial", menuName ="ScriptableObjects/Create new tutorial")]
public class TutorialScriptable : ScriptableObject
{
    public int tutorialIndex;
    public string tutorialText;
}
