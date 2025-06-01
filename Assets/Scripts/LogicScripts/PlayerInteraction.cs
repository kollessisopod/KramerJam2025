using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public DialogueUI dialogueUI;   
    public List<NpcComponent> collidingNpcs = new();
    public List<QuestItemComponent> collidingItems = new();
    public QuestItemComponent currentItem;
    public NpcComponent currentNpc;
    public List<QuestItemComponent> Items= new();
    public QuestItemComponent chosenItem;
    public QuestTrackerScript questTracker;

    private int lineIndex = 0; 
    private bool flag = false;
    private bool isNpcCloser = false;
    private bool outflag = false;

    private void Start()
    {
        questTracker = GameManagerScript.Instance.questTracker;
    }


    void Update()
    {
        currentNpc = FindClosestNpc();
        currentItem = FindClosestItem();
        UpdateAlertMark();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isNpcCloser)
            {
                if (currentNpc != null)
                {
                    var npcData = currentNpc.npcData;

                    if (npcData.dialogueLines == null || npcData.dialogueLines.Count == 0)
                        return;

                    if(questTracker.NpcQuestInteraction(currentNpc, dialogueUI))
                    {
                        outflag = true;
                        return;
                    }

                    if(outflag)
                    {
                        dialogueUI.HideDialogue();
                        outflag = false;
                        return;
                    }

                    if (currentNpc.onLastLine)
                    {
                        if (!flag)
                        {
                            dialogueUI.ShowDialogue(npcData, npcData.lastLine);
                            flag = true;
                        }
                        else
                        {
                            dialogueUI.HideDialogue();
                            ResetDialogueState();
                        }
                        return;
                    }

                    var currentGroup = npcData.dialogueLines[currentNpc.dialogueGroupIndex];

                    // 2. Show current line
                    if (lineIndex < currentGroup.lines.Count)
                    {
                        dialogueUI.ShowDialogue(npcData, currentGroup.lines[lineIndex]);
                        lineIndex++;
                    }
                    else
                    {
                        // 3. Move to next group
                        flag = false;
                        currentNpc.NextDialogue();
                        lineIndex = 0;
                        dialogueUI.HideDialogue();
                    }
                }
            }
            else
            {
                if(currentItem != null)
                {
                    currentItem.HideAlertMark();
                    currentItem.Interact();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Npc"))
        {
            var npcComponent = other.GetComponent<NpcComponent>();
            if (npcComponent != null)
            {
                collidingNpcs.Add(npcComponent);
            }
            else
            {
                Debug.LogWarning("NpcComponent not found on " + other.name);
            }
        }
        else if (other.CompareTag("QuestItem"))
        {
            var itemComponent = other.GetComponent<QuestItemComponent>();
            if (itemComponent != null)
            {
                collidingItems.Add(itemComponent);
                FindClosestItem();
            }
            else
            {
                Debug.LogWarning("QuestItemComponent not found on " + other.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Npc"))
        {
            NpcComponent npc = other.GetComponent<NpcComponent>();
            collidingNpcs.Remove(npc);
            npc.HideAlertMark();

        }
        else if (other.CompareTag("QuestItem"))
        {
            QuestItemComponent item = other.GetComponent<QuestItemComponent>();
            collidingItems.Remove(item);
            item.HideAlertMark();
        }
    }
    private void ResetDialogueState()
    {

        lineIndex = 0;
        flag = false;
    }

    private NpcComponent FindClosestNpc()
    {
        if (collidingNpcs.Count == 0)
            return null;

        NpcComponent closestNpc = null;
        float closestDistance = Mathf.Infinity;

        foreach (var npc in collidingNpcs)
        {
            float distance = Vector2.Distance(transform.position, npc.transform.position);
            npc.HideAlertMark();
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNpc = npc;
            }
        }
        return closestNpc;
    }

    private QuestItemComponent FindClosestItem()
    {
        if (collidingItems.Count == 0)
            return null;
        QuestItemComponent closestItem = null;
        float closestDistance = Mathf.Infinity;
        foreach (var item in collidingItems)
        {
            float distance = Vector2.Distance(transform.position, item.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }
        return closestItem;

    }


    private void UpdateAlertMark()
    {
        // calculate distance of closest item and npc and choose closest

        if (currentNpc != null || currentItem != null)
        {
            if (currentNpc == null)
            {
                currentItem.ShowAlertMark();
                isNpcCloser = false;
            }
            else if (currentItem == null)
            {
                currentNpc.ShowAlertMark();
                isNpcCloser = true;
            }
            else
            {
                float npcDistance = Vector2.Distance(transform.position, currentNpc.transform.position);
                float itemDistance = Vector2.Distance(transform.position, currentItem.transform.position);
                if (npcDistance < itemDistance)
                {
                    currentNpc.ShowAlertMark();
                    currentItem.HideAlertMark();
                    isNpcCloser = true;
                }
                else
                {
                    currentItem.ShowAlertMark();
                    currentNpc.HideAlertMark();
                    isNpcCloser = false;
                }
            }
        }
    }
}