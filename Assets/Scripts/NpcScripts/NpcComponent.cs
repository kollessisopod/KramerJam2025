using UnityEngine;

public class NpcComponent : MonoBehaviour
{
    public string npcName;
    public NpcSO npcData;
    public bool onLastLine = false;
    public int dialogueGroupIndex = 0;
    public GameObject alertMarkObject;
    public AlertMarkScript alertMark;
    public SpriteRenderer npcSpriteRenderer;
    private SpriteRenderer alertMarkSpriteRenderer;

    private void Start()
    {
        alertMark = alertMarkObject.GetComponent<AlertMarkScript>();
        alertMarkSpriteRenderer = alertMarkObject.GetComponent<SpriteRenderer>();
        if (npcData == null)
        {
            Debug.LogWarning("NpcData is not assigned for " + gameObject.name);
            return;
        } 

        npcSpriteRenderer.sprite = npcData.npcSprite;

    }
    public void NextDialogue()
    {
        if (dialogueGroupIndex < npcData.dialogueLines.Count - 1)
        {
            dialogueGroupIndex++;
            onLastLine = false;
        }
        else
        {
            onLastLine = true;
            GameManagerScript.Instance.talkedNpcCount++;
        }
    }
    public void ShowAlertMark()
    {
        if (alertMarkSpriteRenderer != null)
        {
            alertMarkSpriteRenderer.enabled = true; // Show the alert mark
        }
        else
        {
            Debug.LogError("AlertMarkSpriteRenderer is not assigned in the inspector.");
        }
    }
    public void HideAlertMark()
    {
        if (alertMarkSpriteRenderer != null)
        {
            alertMarkSpriteRenderer.enabled = false; // Hide the alert mark
        }
        else
        {
            Debug.LogError("AlertMarkSpriteRenderer is not assigned in the inspector.");
        }
    }

    public void AddDialogue(string message)
    {
        if (npcData == null)
        {
            Debug.LogWarning("NpcData is not assigned for " + gameObject.name);
            return;
        }

        npcData.lastLine = message;

    }
}