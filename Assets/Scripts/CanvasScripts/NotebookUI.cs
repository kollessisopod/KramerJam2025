using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotebookUI : MonoBehaviour
{
    public TMP_Text notebookText;

    public GameObject notebookPanel;
    public GameObject questTrackerObject;
    public QuestTrackerScript questTracker;

    // Start is called before the first frame update
    void Start()
    {
        notebookPanel.SetActive(false); // Hide the notebook panel at the start  
        if (questTrackerObject == null)
        {
            Debug.LogError("QuestTrackerObject is not assigned in the inspector.");
            return;
        }
        questTracker = questTrackerObject.GetComponent<QuestTrackerScript>();
    }

    // Remove the OnEnable method and instead add a public method to open the notebook and update it

    public void OpenNotebook()
    {
        notebookPanel.SetActive(true);
        UpdateNotebook();
    }

    void Update()
    {

    }

    public void UpdateNotebook()
    {
        if (questTracker == null)
        {
            Debug.LogError("QuestTracker is not assigned or not found in the player object.");
            notebookText.text = "Quest Tracker not found.";
            return;
        }

        List<Quest> quests = questTracker.Quests;
        if (quests == null || quests.Count == 0)
        {
            notebookText.text = "No quests available.";
            return;
        }

        string compiledText = "";

        foreach (Quest quest in quests)
        {
            if (!quest.isActive) continue;

            if (quest.isCompleted)
            {
                // Display as completed with green color and strikethrough
                compiledText += $"<s>{quest.questDescription}</s>\n\n";
            }
            else
            {
                compiledText += $"<b>{quest.questDescription}\n\n";
            }
        }

        notebookText.text = compiledText;
        Debug.Log("meow : " + compiledText);
    }

}
