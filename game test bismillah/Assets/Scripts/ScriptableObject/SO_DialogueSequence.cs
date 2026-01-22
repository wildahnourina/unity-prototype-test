using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object/Game Setup/Dialogue Data/New Line", fileName = "Line - ")]
public class SO_DialogueSequence : ScriptableObject
{
    public SO_DialogueSpeaker speaker;
    public string[] lines;
}
