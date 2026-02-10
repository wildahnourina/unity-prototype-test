using UnityEngine;

public class Object_Interactable_Note : Object_Interactable
{
    [TextArea]
    [SerializeField] private string noteContent;

    public override void Interact()
    {
        AudioManager.instance.PlayGlobalSFX("note_look");
        ui.OpenNoteUI(noteContent);
    }

    protected override string GetPromptText()
    {
        return "(E) Interact";
    }
}
