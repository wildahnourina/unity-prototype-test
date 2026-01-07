using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;

    private PlayerInputSet input;

    public UI_Dialogue dialogueUI { get; private set; }

    private void Awake()
    {
        instance = this;

        dialogueUI = GetComponentInChildren<UI_Dialogue>(true);
    }

    public void SetupControlsUI(PlayerInputSet inputSet)
    {
        input = inputSet;

        input.UI.DialogueInteract.performed += ctx =>
        {
            if (dialogueUI.gameObject.activeInHierarchy)
                dialogueUI.DialogueInteract();
        };
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
