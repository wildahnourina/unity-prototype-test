using UnityEngine;
using TMPro;

public abstract class Object_Interactable : MonoBehaviour, IInteractable
{
    protected Player player;

    [SerializeField] private GameObject interactToolTip;
    private TMP_Text promptText;

    private void Awake()
    {
        promptText = interactToolTip.GetComponentInChildren<TMP_Text>(true);

        Debug.Log(promptText);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Player player))
            return;
        this.player = player;

        ShowPrompt();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && this.player == player)
            this.player = null;

        HidePrompt();
    }

    protected void ShowPrompt()
    {
        if (interactToolTip == null || promptText == null) return;

        if (player == null)
            return;

        IInteractable closest = player.GetClosestInteractable();

        if (closest != (IInteractable)this)
        {
            interactToolTip.SetActive(false);
            return;
        }

        interactToolTip.SetActive(true);
        RefreshPrompt();
    }

    protected void HidePrompt()
    {
        if (interactToolTip == null) return;

        interactToolTip.SetActive(false);
    }

    protected void RefreshPrompt()
    {
        if (promptText == null) return;

        promptText.text = GetPromptText();
    }

    protected abstract string GetPromptText();
    public abstract void Interact();
}
