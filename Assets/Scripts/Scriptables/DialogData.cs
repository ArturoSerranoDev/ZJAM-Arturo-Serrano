using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SpeakerType { LeftSpeaker, RightSpeaker };

[CreateAssetMenu(menuName = "Create DialogData", fileName = "DialogData", order = 0)]
public class DialogData : ScriptableObject
{
    public List<Dialog> levelDialog;
}

[System.Serializable]
public class Dialog
{
    public SpeakerType speaker;
    public string text;
    public Sprite dialogImage;
}