using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "CreateSO/NPC")]
public class NpcSO : ScriptableObject
{
    public string npcName;
    public Sprite npcSprite;
    public Sprite npcPortrait;

    public List<DialogueGroup> dialogueLines = new();
    public string lastLine;
}

[System.Serializable]
public class DialogueGroup
{
    public List<string> lines = new();
}