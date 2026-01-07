using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_Dialogue : MonoBehaviour
{
    private UI ui;

    [SerializeField] private Image speakerPortrait;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Space]
    [SerializeField] private float textSpeed = .1f;

    private SO_DialogueSequence currentSequence;
    private int lineIndex;
    private Coroutine typeTextCo;
    private bool isTyping;
    private bool waitingInput;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
    }

    public void PlayDialogue(SO_DialogueSequence sequence)
    {
        currentSequence = sequence;
        lineIndex = 0;

        SetupSpeaker(sequence.speaker);
        PlayCurrentLine();
    }

    public void DialogueInteract()
    {
        // skip typing
        if (isTyping)
        {
            FinishTyping();
            return;
        }

        if (!waitingInput)
            return;

        NextLine();
    }

    private void PlayCurrentLine()
    {
        if (typeTextCo != null)
            StopCoroutine(typeTextCo);

        typeTextCo = StartCoroutine(TypeTextCo(currentSequence.lines[lineIndex]));
    }

    private IEnumerator TypeTextCo(string line)
    {
        yield return null;//kasih jeda 1 frame biar gak langsung fulltext

        isTyping = true;
        waitingInput = false;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        FinishTyping();
    }

    private void FinishTyping()
    {
        if (typeTextCo != null)
            StopCoroutine(typeTextCo);

        dialogueText.text = currentSequence.lines[lineIndex];
        typeTextCo = null;

        isTyping = false;
        waitingInput = true;
    }

    private void NextLine()
    {
        waitingInput = false;
        lineIndex++;

        if (lineIndex >= currentSequence.lines.Length)
        {
            Close();
            return;
        }

        PlayCurrentLine();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        ui.StopPlayerControls(false);
    }

    private void SetupSpeaker(SO_DialogueSpeaker speaker)
    {
        if (speaker == null)
        {
            speakerName.text = "";
            speakerPortrait.gameObject.SetActive(false);
            return;
        }

        speakerName.text = speaker.speakerName;
        speakerPortrait.sprite = speaker.speakerPortrait;
        speakerPortrait.gameObject.SetActive(true);
    }
}
