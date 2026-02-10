using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;

    private PlayerInputSet input;

    public UI_Dialogue dialogueUI { get; private set; }
    public UI_Note noteUI { get; private set; }

    private void Awake()
    {
        instance = this;

        dialogueUI = GetComponentInChildren<UI_Dialogue>(true);
        noteUI = GetComponentInChildren<UI_Note>(true);
    }

    public void SetupControlsUI(PlayerInputSet inputSet)
    {
        input = inputSet;

        input.UI.Interact.performed += ctx =>
        {
            if (dialogueUI.gameObject.activeInHierarchy)
                dialogueUI.DialogueInteract();
            if (noteUI.gameObject.activeInHierarchy)
                noteUI.Hide();
        };
    }

    public void OpenNoteUI(string text)
    {
        StopPlayerControls(true);
        noteUI.Show(text);
    }

    public void OpenDialogueUI(SO_DialogueSequence firstLine)
    {
        StopPlayerControls(true);

        dialogueUI.gameObject.SetActive(true);
        dialogueUI.PlayDialogue(firstLine);
    }

    public void StopPlayerControls(bool stopControls)
    {
        if (stopControls)
            input.Player.Disable();
        else
            input.Player.Enable();
    }
}
