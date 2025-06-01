using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class NpcSOEditorWindow : EditorWindow
{
    private NpcSO npc;
    private Vector2 scrollPos;

    [MenuItem("Tools/NPC Editor")]
    public static void ShowWindow()
    {
        GetWindow<NpcSOEditorWindow>("NPC Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        npc = (NpcSO)EditorGUILayout.ObjectField("Select NPC", npc, typeof(NpcSO), false);

        if (npc == null)
        {
            if (GUILayout.Button("Create New NPC"))
            {
                CreateNewNpc();
            }

            EditorGUILayout.HelpBox("Select an NPC ScriptableObject or create a new one.", MessageType.Info);
            return;
        }

        EditorGUILayout.Space();
        EditorGUI.BeginChangeCheck();

        npc.npcName = EditorGUILayout.TextField("NPC Name", npc.npcName);
        npc.npcSprite = (Sprite)EditorGUILayout.ObjectField("NPC Sprite", npc.npcSprite, typeof(Sprite), false);
        npc.npcPortrait = (Sprite)EditorGUILayout.ObjectField("NPC Portrait", npc.npcPortrait, typeof(Sprite), false);

        EditorGUILayout.LabelField("Dialogue Groups:", EditorStyles.boldLabel);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(300));

        if (npc.dialogueLines == null)
            npc.dialogueLines = new List<DialogueGroup>();

        int groupToRemove = -1;

        for (int g = 0; g < npc.dialogueLines.Count; g++)
        {
            var group = npc.dialogueLines[g];
            if (group == null)
            {
                group = new DialogueGroup();
                npc.dialogueLines[g] = group;
            }

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Group {g + 1}", EditorStyles.boldLabel);

            if (group.lines == null)
                group.lines = new List<string>();

            int lineToRemove = -1;
            for (int l = 0; l < group.lines.Count; l++)
            {
                EditorGUILayout.BeginHorizontal();
                group.lines[l] = EditorGUILayout.TextField($"Line {l + 1}", group.lines[l]);
                if (GUILayout.Button("X", GUILayout.Width(20)))
                    lineToRemove = l;
                EditorGUILayout.EndHorizontal();
            }

            if (lineToRemove >= 0 && lineToRemove < group.lines.Count)
                group.lines.RemoveAt(lineToRemove);

            if (GUILayout.Button("Add Line to Group"))
                group.lines.Add("");

            if (GUILayout.Button("Remove Group"))
                groupToRemove = g;

            EditorGUILayout.EndVertical();
        }

        if (groupToRemove >= 0 && groupToRemove < npc.dialogueLines.Count)
            npc.dialogueLines.RemoveAt(groupToRemove);

        if (GUILayout.Button("Add New Dialogue Group"))
        {
            npc.dialogueLines.Add(new DialogueGroup());
        }

        EditorGUILayout.EndScrollView();

        npc.lastLine = EditorGUILayout.TextField("Last Line", npc.lastLine);

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(npc);
            AssetDatabase.SaveAssets();
        }
    }

    private void CreateNewNpc()
    {
        string folderPath = "Assets/Resources/NPCs";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "NPCs");
        }

        string defaultName = "NewNpc";
        string npcName = EditorUtility.SaveFilePanelInProject(
            "Create New NPC",
            defaultName,
            "asset",
            "Enter a name for the new NPC",
            folderPath
        );

        if (string.IsNullOrEmpty(npcName))
            return;

        npc = ScriptableObject.CreateInstance<NpcSO>();

        AssetDatabase.CreateAsset(npc, npcName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = npc;
    }

}
