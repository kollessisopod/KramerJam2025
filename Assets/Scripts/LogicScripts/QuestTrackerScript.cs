using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrackerScript : MonoBehaviour
{
    public MTGQuest mtgQuest;
    public PushUpQuest pushUpQuest;
    public JustDanceQuest justDanceQuest;
    public SleepQuest sleepQuest;
    public WallpaperQuest wallpaperQuest;
    public NoodleQuest noodleQuest;
    public Quest socialQuest;
    public List<QuestItemComponent> QuestItemComponents;
    public QuestItemComponent mtgBag;
    public QuestItemComponent redbull;
    public QuestItemComponent remote;
    public QuestItemComponent bed;
    public QuestItemComponent maliLaptop;
    public QuestItemComponent noodle;
    private PlayerInteraction playerInteraction;
    private PostQuestEffectsScript postQuestEffects;
    private bool wonState = false;
    public List<Quest> Quests => new() { mtgQuest, pushUpQuest, justDanceQuest, sleepQuest, wallpaperQuest, noodleQuest, socialQuest };
    public Image nahWallpaper;
    void Start()
    {
        //Singleton
        mtgQuest = new MTGQuest(mtgBag, new List<string> { "kata", "omer", "eymen", "sadeali" });
        pushUpQuest = new PushUpQuest(redbull, new List<string> { "ugur", "kagan", "kata" });
        justDanceQuest = new JustDanceQuest(remote, new List<string> { "mali", "deniz" });
        sleepQuest = new SleepQuest(bed, new List<string> { "egehan", "bugra" });
        wallpaperQuest = new WallpaperQuest(maliLaptop, new List<string> { "kagan", "egea", "alren" });
        noodleQuest = new NoodleQuest(noodle, new List<string> { "ege" });
        socialQuest = new Quest { isActive = true, questDescription = "Tanımadığım insanlarla tanışabilirim." };

        postQuestEffects = GameManagerScript.Instance.postQuestEffects;

        wonState = GameManagerScript.Instance.wonState;
        playerInteraction = GameManagerScript.Instance.player.GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        if (!mtgQuest.isActive)
        {
            if(mtgBag.found)
            {
                mtgQuest.isActive = true;
                Debug.Log("Quest is now active: " + mtgQuest.questDescription);
            }
        }
        else if(!mtgQuest.isCompleted)
        {
            if(mtgQuest.kataBool && mtgQuest.omerBool && mtgQuest.eymenBool && mtgQuest.sadealiBool)
            {
                mtgQuest.CompleteQuest();
                Debug.Log("Quest completed: " + mtgQuest.questDescription);
                GameManagerScript.Instance.player.GetComponent<PlayerInteraction>().Items.Remove(mtgBag);
                
                postQuestEffects.ProcessPostQuest(mtgQuest);
            }
        }

        if(!pushUpQuest.isActive)
        {
            if (redbull.found)
            {
                pushUpQuest.isActive = true;
                Debug.Log("Push Up Quest is now active: " + pushUpQuest.questDescription);
            }
        }
        else if (!pushUpQuest.isCompleted)
        {
            if (pushUpQuest.kataBool && pushUpQuest.ugurBool && pushUpQuest.kaganBool)
            {
                pushUpQuest.CompleteQuest();
                Debug.Log("Push Up Quest completed: " + pushUpQuest.questDescription);
                GameManagerScript.Instance.player.GetComponent<PlayerInteraction>().Items.Remove(redbull);
            }
        }

        if (!justDanceQuest.isActive)
        {
            if (remote.found)
            {
                justDanceQuest.isActive = true;
                Debug.Log("Just Dance Quest is now active: " + justDanceQuest.questDescription);
            }
        }
        else if (!justDanceQuest.isCompleted)
        {
            if (justDanceQuest.maliBool && justDanceQuest.denizBool)
            {
                justDanceQuest.CompleteQuest();
                Debug.Log("Just Dance Quest completed: " + justDanceQuest.questDescription);
                GameManagerScript.Instance.player.GetComponent<PlayerInteraction>().Items.Remove(remote);
                postQuestEffects.ProcessPostQuest(justDanceQuest);

            }
        }

        if (!sleepQuest.isActive)
        {
            if (bed.found)
            {
                sleepQuest.isActive = true;
                Debug.Log("Sleep Quest is now active: " + sleepQuest.questDescription);
            }
        }
        else if (!sleepQuest.isCompleted)
        {
            if (sleepQuest.egehanBool && sleepQuest.bugraBool)
            {
                sleepQuest.CompleteQuest();
                Debug.Log("Sleep Quest completed: " + sleepQuest.questDescription);
                GameManagerScript.Instance.player.GetComponent<PlayerInteraction>().Items.Remove(bed);
                postQuestEffects.ProcessPostQuest(sleepQuest);

            }
        }


        if (!wallpaperQuest.isActive)
        {
            if (maliLaptop.found)
            {
                wallpaperQuest.isActive = true;
                Debug.Log("Wallpaper Quest is now active: " + wallpaperQuest.questDescription);
            }
        }
        else if (!wallpaperQuest.isCompleted)
        {
            if (wallpaperQuest.kaganBool && wallpaperQuest.egeArdaBool && wallpaperQuest.alrenBool)
            {
                wallpaperQuest.CompleteQuest();
                Debug.Log("Wallpaper Quest completed: " + wallpaperQuest.questDescription);
                GameManagerScript.Instance.player.GetComponent<PlayerInteraction>().Items.Remove(maliLaptop);
                postQuestEffects.ProcessPostQuest(wallpaperQuest);
                GameManagerScript.Instance.cutscenePanel.GetComponent<CutsceneUI>().ShowCutscene(nahWallpaper, "Anlamlı");

            }
        }

        if (!noodleQuest.isActive)
        {
            if (noodle.found)
            {
                noodleQuest.isActive = true;
                Debug.Log("Noodle Quest is now active: " + noodleQuest.questDescription);
            }
        }
        else if (!noodleQuest.isCompleted)
        {
            if (noodleQuest.egeBool)
            {
                noodleQuest.CompleteQuest();
                Debug.Log("Noodle Quest completed: " + noodleQuest.questDescription);
                GameManagerScript.Instance.player.GetComponent<PlayerInteraction>().Items.Remove(noodle);
                postQuestEffects.ProcessPostQuest(noodleQuest);
                playerInteraction.cutsceneIncoming = true;
                playerInteraction.questKeyword = "noodle";


            }
        }

        if(!socialQuest.isActive)
        {
            socialQuest.isActive = true;
        } else if(!socialQuest.isCompleted)
        {
            if(GameManagerScript.Instance.talkedNpcCount >= GameManagerScript.Instance.allNpcs.Count)
            {
                socialQuest.CompleteQuest();
                Debug.Log("Social Quest is completed: " + socialQuest.questDescription);
            }
        }

        // Win Condition
        if (mtgQuest.isCompleted &&
            pushUpQuest.isCompleted &&
            justDanceQuest.isCompleted &&
            sleepQuest.isCompleted &&
            wallpaperQuest.isCompleted &&
            noodleQuest.isCompleted && 
            socialQuest.isCompleted && 
            !wonState)
        {
            Debug.Log("All quests completed! You win the game!");
            wonState = true;
            GameManagerScript.Instance.wonState = true;
            GameManagerScript.Instance.jammerCount--;
            GameManagerScript.Instance.WinMeow();
        }
    }


    public bool NpcQuestInteraction(NpcComponent npc, DialogueUI dialogueUI)
    {
        if(playerInteraction.Items.Count == 0)
        {
            return false;
        }
        bool hasBag = playerInteraction.Items.Contains(mtgBag);
        bool hasRedbull = playerInteraction.Items.Contains(redbull);
        bool hasRemote = playerInteraction.Items.Contains(remote);
        bool hasBed = playerInteraction.Items.Contains(bed);
        bool hasMaliLaptop = playerInteraction.Items.Contains(maliLaptop);
        bool hasNoodle = playerInteraction.Items.Contains(noodle);

        if (npc.npcData.name == "kata" && hasBag)
        {
            if (!mtgQuest.kataBool)
            {
                Debug.Log($"{npc.npcData.name}");
                dialogueUI.ShowDialogue(npc.npcData, "TOPLA ÖMERİ SADEALİYİ EYMENİ MAGIC ATIYOZ");
                mtgQuest.kataBool = true;
                Debug.Log("Kata's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "omer" && hasBag)
        {
            if (!mtgQuest.omerBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Magic mi, olur valla döner.");
                mtgQuest.omerBool = true;
                Debug.Log("Omer's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "eymen" && hasBag)
        {
            if (!mtgQuest.eymenBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Ya çoluğunu çocuğunu, neyse magic okey");
                mtgQuest.eymenBool = true;
                Debug.Log("Eymen's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "sadeali" && hasBag)
        {
            if (!mtgQuest.sadealiBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Tamam abi siz oynuyosanız gelirim magic");
                mtgQuest.sadealiBool = true;
                Debug.Log("Sadeali's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "ugur" && hasRedbull)
        {
            if (!pushUpQuest.ugurBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Abi ne sınavı ya HAA ŞINAV MI ABİ BİZİM ŞINAV YARIŞMASI VARDI ALOOO");
                pushUpQuest.ugurBool = true;
                Debug.Log("Ugur's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "kagan" && hasRedbull)
        {
            if (!pushUpQuest.kaganBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, ":D Amınakoyim yaa, Tamam lanet olsun yapalım o şınav yarışmasını da");
                pushUpQuest.kaganBool = true;
                Debug.Log("Kagan's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "kata" && hasRedbull)
        {
            if (!pushUpQuest.kataBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Ben diyetteyim abi bu şekerli *lıkır lıkır* ŞINAV YARIŞMASI LETS GOO");
                pushUpQuest.kataBool = true;
                Debug.Log("Kata's push up quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "mali" && hasRemote)
        {
            if (!justDanceQuest.maliBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Abi dinlenmek bana fark etmez amınakoim Deniz başliyak hadi");
                justDanceQuest.maliBool = true;
                Debug.Log("Mali's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "deniz" && hasRemote)
        {
            if (!justDanceQuest.denizBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Free kulaklık Mali hazır olsun");
                justDanceQuest.denizBool = true;
                Debug.Log("Deniz's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "egehan" && hasBed)
        {
            if (!sleepQuest.egehanBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Ya yatak nereye gitti alo");
                sleepQuest.egehanBool = true;
                Debug.Log("Egehan's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "bugra" && hasBed)
        {
            if (!sleepQuest.bugraBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Yatağını nasıl kaybedebilirsin ki");
                sleepQuest.bugraBool = true;
                Debug.Log("Bugra's quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "kagan" && hasMaliLaptop)
        {
            if (!wallpaperQuest.kaganBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Toplan cabuk Malinin laptop açık :D");
                wallpaperQuest.kaganBool = true;
                Debug.Log("Kagan's wallpaper quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "egea" && hasMaliLaptop)
        {
            if (!wallpaperQuest.egeArdaBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Gerçek patron zenci işi type shit");
                wallpaperQuest.egeArdaBool = true;
                Debug.Log("Egea's wallpaper quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "alren" && hasMaliLaptop)
        {
            if (!wallpaperQuest.alrenBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "Eşşek laptopu");
                wallpaperQuest.alrenBool = true;
                Debug.Log("Alren's wallpaper quest interaction completed.");
                return true;
            }
        }
        if (npc.npcData.name == "ege" && hasNoodle)
        {
            if (!noodleQuest.egeBool)
            {
                dialogueUI.ShowDialogue(npc.npcData, "AYOOOOOO NOODLE EDIT");
                noodleQuest.egeBool = true;
                Debug.Log("Ege's noodle quest interaction completed.");
                return true;
            }
        }
        return false;
    }
}

public class Quest
{
    public string questDescription;
    public bool isActive = false;
    public bool isCompleted = false;
    public string questKeyword = "";

    public void CompleteQuest()
    {
        isCompleted = true;
    }
}

public class MTGQuest : Quest
{
    public QuestItemComponent bag;
    public List<string> questNpcsNames;
    public List<NpcComponent> questNpcs;
    public bool kataBool = false;
    public bool omerBool = false;
    public bool eymenBool = false;
    public bool sadealiBool = false;
    public MTGQuest(QuestItemComponent bag, List<string> questNpcsNames)
    {

        this.questNpcsNames = questNpcsNames;
        this.bag = bag;
        bag.quest = this; 
        questNpcs = new List<NpcComponent>();
        // add npcs that have the names of npcs names
        foreach (string npcName in questNpcsNames)
        {
            NpcComponent npc = GameManagerScript.Instance.allNpcs.Find(n => n.GetComponent<NpcComponent>().npcName == npcName)?.GetComponent<NpcComponent>();
            if (npc != null)
            {
                questNpcs.Add(npc);
            }
            else
            {
                Debug.LogWarning($"NPC with name {npcName} not found in the scene.");
            }
        }
        questDescription = "Magic desteli çanta buldum. Kata, Ömer, SadeAli ve Eymen magic oynamak isteyebilirler.";
        questKeyword = "mtg";
    }
    
}

public class PushUpQuest : Quest
{
    public QuestItemComponent redbull;
    public List<string> questNpcsNames;
    public List<NpcComponent> questNpcs;

    public bool ugurBool = false;
    public bool kaganBool = false;
    public bool kataBool = false;

    //constructor
    public PushUpQuest(QuestItemComponent redbull, List<string> questNpcsNames)
    {
        this.questNpcsNames = questNpcsNames;
        questNpcs = new List<NpcComponent>();
        // add npcs that have the names of npcs names
        foreach (string npcName in questNpcsNames)
        {
            NpcComponent npc = GameManagerScript.Instance.allNpcs.Find(n => n.GetComponent<NpcComponent>().npcName == npcName)?.GetComponent<NpcComponent>();
            if (npc != null)
            {
                questNpcs.Add(npc);
            }
            else
            {
                Debug.LogWarning($"NPC with name {npcName} not found in the scene.");
            }
        }

        this.redbull = redbull;
        redbull.quest = this;
        questDescription = "Redbull buldum. Kata, Uğur ve Kağan'a yeterince enerji vermeye yetebilir.";
        questKeyword = "pushup";
    }
}

public class JustDanceQuest : Quest
{
    public QuestItemComponent remote;
    public List<string> questNpcsNames;
    public List<NpcComponent> questNpcs;
    public bool maliBool = false;
    public bool denizBool = false;
    public JustDanceQuest(QuestItemComponent remote, List<string> questNpcsNames)
    {
        this.questNpcsNames = questNpcsNames;
        questNpcs = new List<NpcComponent>();
        // add npcs that have the names of npcs names
        foreach (string npcName in questNpcsNames)
        {
            NpcComponent npc = GameManagerScript.Instance.allNpcs.Find(n => n.GetComponent<NpcComponent>().npcName == npcName)?.GetComponent<NpcComponent>();
            if (npc != null)
            {
                questNpcs.Add(npc);
            }
            else
            {
                Debug.LogWarning($"NPC with name {npcName} not found in the scene.");
            }
        }
        this.remote = remote;
        remote.quest = this; // Assign the quest to the remote item
        questDescription = "Nintendo kumandası buldum. Mali ve Deniz'in Just Dance finali yaklaşıyor.";
        questKeyword = "justdance";

    }
}

public class SleepQuest : Quest
{
    public QuestItemComponent bed;
    public List<string> questNpcsNames;
    public List<NpcComponent> questNpcs;
    public bool egehanBool = false;
    public bool bugraBool = false;
    public SleepQuest(QuestItemComponent bed, List<string> questNpcsNames)
    {
        this.questNpcsNames = questNpcsNames;
        questNpcs = new List<NpcComponent>();

        foreach (string npcName in questNpcsNames)
        {
            NpcComponent npc = GameManagerScript.Instance.allNpcs.Find(n => n.GetComponent<NpcComponent>().npcName == npcName)?.GetComponent<NpcComponent>();
            if (npc != null)
            {
                questNpcs.Add(npc);
            }
            else
            {
                Debug.LogWarning($"NPC with name {npcName} not found in the scene.");
            }
        }
        this.bed = bed;
        bed.quest = this; // Assign the quest to the bed item
        questDescription = "Boşta son kalan yatağı kaptım. Egehan ve Buğra uykulu görünüyordu.";
        questKeyword = "sleep";
    }
}

public class WallpaperQuest : Quest
{
    public QuestItemComponent maliLaptop;
    public List<string> questNpcsNames;
    public List<NpcComponent> questNpcs;
    public bool kaganBool = false;
    public bool egeArdaBool = false;
    public bool alrenBool = false;
    public WallpaperQuest(QuestItemComponent maliLaptop, List<string> questNpcsNames)
    {
        this.questNpcsNames = questNpcsNames;
        questNpcs = new List<NpcComponent>();
        foreach (string npcName in questNpcsNames)
        {
            NpcComponent npc = GameManagerScript.Instance.allNpcs.Find(n => n.GetComponent<NpcComponent>().npcName == npcName)?.GetComponent<NpcComponent>();
            if (npc != null)
            {
                questNpcs.Add(npc);
            }
            else
            {
                Debug.LogWarning($"NPC with name {npcName} not found in the scene.");
                Debug.Log(GameManagerScript.Instance.allNpcs.Count + " NPCs found in the scene.");

            }
        }
        this.maliLaptop = maliLaptop;
        maliLaptop.quest = this; // Assign the quest to the wallpaper item
        questDescription = "Mali'nin laptopu açık kalmış. Kağan, Alren ve Ege Arda güzel bi sürpriz yapabilir.";
        questKeyword = "wallpaper";
    }
}

public class NoodleQuest : Quest
{
    public QuestItemComponent noodle;
    public List<string> questNpcsNames;
    public List<NpcComponent> questNpcs;
    public bool egeBool = false;
    public NoodleQuest(QuestItemComponent noodle, List<string> questNpcsNames)
    {
        this.questNpcsNames = questNpcsNames;
        questNpcs = new List<NpcComponent>();
        foreach (string npcName in questNpcsNames)
        {
            NpcComponent npc = GameManagerScript.Instance.allNpcs.Find(n => n.GetComponent<NpcComponent>().npcName == npcName)?.GetComponent<NpcComponent>();
            if (npc != null)
            {
                questNpcs.Add(npc);
            }
            else
            {
                Debug.LogWarning($"NPC with name {npcName} not found in the scene.");
            }
        }
        this.noodle = noodle;
        noodle.quest = this; 
        questDescription = "Ege bu Noodle'a edit yapmak istiyor, hell yeah.";
        questKeyword = "noodle";
    }

}