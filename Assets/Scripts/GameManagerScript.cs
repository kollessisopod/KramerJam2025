using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    public GameObject mainCamera;
    public GameObject globalLight;
    public GameObject canvas;
    public GameObject hud;
    public GameObject panel;
    public GameObject winPanel;
    public GameObject player;
    public GameObject questTrackerObject;
    public GameObject notebookPanel;
    public GameObject cutscenePanel;
    public GameObject NpcPrefab;
    public QuestTrackerScript questTracker;
    public PostQuestEffectsScript postQuestEffects;
    public List<GameObject> allNpcs;
    public int npcCount => allNpcs.Count;
    public int talkedNpcCount = 0;
    public int jammerCount = 80;
    public bool wonState = false;
    private CutsceneUI cutsceneUI;

    private void Awake()
    {

        // Ensure only one instance of GameManagerScript exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        hud.SetActive(true);
        canvas.SetActive(true);
        globalLight.SetActive(true);
        mainCamera.SetActive(true);

        cutsceneUI = cutscenePanel.GetComponent<CutsceneUI>();

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found. Please ensure there is a GameObject with the 'Player' tag in the scene.");
        }
        allNpcs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Npc"));
        if (allNpcs.Count == 0)
        {
            Debug.LogWarning("No NPCs found in the scene. Please ensure there are GameObjects with the 'Npc' tag.");
        }

        // Get all NpcSO's 
        List<NpcSO> npcSOs = new List<NpcSO>(Resources.LoadAll<NpcSO>("NPCs"));
        if (npcSOs.Count == 0)
        {
            Debug.LogWarning("No NPC ScriptableObjects found in the Resources/NPCs directory.");
        }
        else
        {
            foreach (NpcSO npcSO in npcSOs)
            {
                //find same named npc in allNpcs
                GameObject npcObject = allNpcs.Find(npc => npc.GetComponent<NpcComponent>().npcName == npcSO.npcName);
                if (npcObject != null)
                {
                    NpcComponent npcComponent = npcObject.GetComponent<NpcComponent>();
                    if (npcComponent != null)
                    {
                        npcComponent.npcData = npcSO;
                        Debug.Log($"Assigned NPC data for {npcSO.npcName} to the NPC GameObject.");
                    }
                    else
                    {
                        Debug.LogWarning($"NpcComponent not found on GameObject {npcObject.name}. NPC data not assigned.");
                    }
                }
                else
                {
                    Debug.LogWarning($"No GameObject found with the name {npcSO.npcName} to assign NPC data.");
                }
            }
        }
        questTracker = questTrackerObject.GetComponent<QuestTrackerScript>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeInHierarchy || notebookPanel.activeInHierarchy || cutscenePanel.activeInHierarchy)
        {
            // Pause the game when the canvas is active
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        if (cutscenePanel.activeInHierarchy)
        {
            cutsceneUI.ShowCutscene(cutsceneUI.image, "Cutscene is active. Press Space to continue.");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Toggle the notebook panel when Tab is pressed
            if (notebookPanel != null && !panel.activeInHierarchy && !cutscenePanel.activeInHierarchy)
            {
                if(!notebookPanel.activeInHierarchy)
                {
                    notebookPanel.GetComponent<NotebookUI>().OpenNotebook();
                }
                else
                {
                    notebookPanel.SetActive(false);
                }


            }
            else
            {
                Debug.LogWarning("Notebook panel is not assigned in the GameManagerScript.");
            }

        }
    }

    public void StartCutscene()
    {
        if (cutscenePanel != null)
        {
            cutscenePanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game during the cutscene
        }
        else
        {
            Debug.LogWarning("Cutscene panel is not assigned in the GameManagerScript.");
        }
    }

    public void AddTalkedNpc()
    {
        talkedNpcCount++;
        Debug.Log($"Total NPCs talked to: {talkedNpcCount}");
        if(talkedNpcCount >= npcCount)
        {
            Debug.Log("All NPCs have been talked to.");
            // You can add additional logic here, such as triggering an event or changing the game state.
        }
    }
    public void WinMeow()
    {
        wonState = true;
        Debug.Log("You won the game!");
        winPanel.SetActive(true);
    }

    public void FinishGame()
    {
        SceneManager.LoadScene("VictoryScene");
    }


}
