using UnityEngine;
using TMPro;

public abstract class Object_Interactable : MonoBehaviour, IInteractable
{
    protected UI ui;
    protected Player player;

    [SerializeField] private GameObject interactToolTip;
    private TMP_Text promptText;

    protected virtual void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        promptText = interactToolTip.GetComponentInChildren<TMP_Text>(true);
        interactToolTip.SetActive(false);
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Player player))
            return;
        this.player = player;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && this.player == player)
            this.player = null;
    }

    public void ShowPrompt()
    {
        if (interactToolTip == null || promptText == null) return;

        interactToolTip.SetActive(true);
        RefreshPrompt();
    }

    public void HidePrompt()
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
