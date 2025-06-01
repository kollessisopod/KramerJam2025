using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;     // The entire panel
    public Image portraitImage;          // Big portrait on left
    public TMP_Text npcNameText;         // NPC name text
    public TMP_Text dialogueText;        // Dialogue content text

    private void Awake()
    {
        HideDialogue();
    }

    private void Start()
    {

    }

    public void ShowDialogue(NpcSO npc, string dialogueLine)
    {
        dialoguePanel.SetActive(true);

        // Set NPC data
        npcNameText.text = npc.npcName;

        if (npc.npcPortrait != null)
            portraitImage.sprite = npc.npcPortrait;
        else
            portraitImage.sprite = null;  

        dialogueText.text = dialogueLine;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}