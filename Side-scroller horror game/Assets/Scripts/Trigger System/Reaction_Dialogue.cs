using UnityEngine;

public class Reaction_Dialogue : TriggerReaction
{
    private UI ui;
    [SerializeField] private SO_DialogueSequence firstDialogueLine;

    private void Start()
    {
        ui = FindFirstObjectByType<UI>();
    }

    protected override void OnTriggered(TriggerContext ctx)
    {
        ui.OpenDialogueUI(firstDialogueLine);
    }
}
