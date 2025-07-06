using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PostQuestEffectsScript : MonoBehaviour
{
    public List<NpcComponent> allNpcs = new();
    public GameObject thatOneSpesificTable;
    public GameObject laptop;
    public GameObject noodle;


    // Start is called before the first frame update
    void Start()
    {
        allNpcs = GetComponent<GameManagerScript>().allNpcs.ConvertAll(npc => npc.GetComponent<NpcComponent>());
        GameManagerScript.Instance.postQuestEffects = this;
    }

    public void ProcessPostQuest(Quest quest)
    {
        if (quest == null)
        {
            Debug.LogWarning("Quest is null in ProcessPostQuest");
            return;
        }


        switch (quest.questKeyword)
        {
            case "mtg":
                Debug.Log("Processing post quest effects for mtg quest");
                var kata = allNpcs.Find(npc => npc.npcName == "kata");
                var eymen = allNpcs.Find(npc => npc.npcName == "eymen");
                var omer = allNpcs.Find(npc => npc.npcName == "omer");
                var sadeali = allNpcs.Find(npc => npc.npcName == "sadeali");

                SetPositionWithMtgOffset(kata.transform, -2.59f, 3.77f);
                SetPositionWithMtgOffset(eymen.transform, -2.7f, 3.32f);
                SetPositionWithMtgOffset(omer.transform, -1.24f, 3.79f);
                SetPositionWithMtgOffset(sadeali.transform, -1.16f, 3.41f);
                kata.AddDialogue("Motorumu çalıştırıyorum.");
                omer.AddDialogue("Burdan sonsuz response var mı");
                eymen.AddDialogue("Tamam abi çoluğuna çocuğuna eldrazi indireyim");
                sadeali.AddDialogue("Bi saniye");

                thatOneSpesificTable.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Items/Table with cardes");
                break;

            case "justdance":
                Debug.Log("Processing post quest effects for justdance quest");
                var mali = allNpcs.Find(npc => npc.npcName == "mali");
                var deniz = allNpcs.Find(npc => npc.npcName == "deniz");
                var samet = allNpcs.Find(npc => npc.npcName == "samet");
                var goksel = allNpcs.Find(npc => npc.npcName == "goksel");

                SetPositionWithJustDanceOffset(mali.transform, 1.11f, 5.35f);
                SetPositionWithJustDanceOffset(deniz.transform, 1.762f, 5.01f);
                SetPositionWithJustDanceOffset(goksel.transform, -0.183f, 5.114f);
                SetPositionWithJustDanceOffset(samet.transform, 3.214f, 5.037f);
                mali.AddDialogue("JUDAAA JUDAAA AA AAASS");
                deniz.AddDialogue("BOOMBAYAAHH");
                samet.AddDialogue("BOMBA BOMBA NOKTA COM");
                goksel.AddDialogue("Bitse de malinin eski kulaklığına konsam");
                break;

            case "noodle":
                var ege = allNpcs.Find(npc => npc.npcName == "ege");
                var dogukan = allNpcs.Find(npc => npc.npcName == "dogukan");
                
                SetPositionWithNoodleOffset(ege.transform, 2.029f, -3.601f);
                SetPositionWithNoodleOffset(dogukan.transform, 3.263f, -3.780f);
                
                dogukan.AddDialogue("Never gonna let you down");
                ege.AddDialogue("Never gonna give you up");

                noodle.transform.position = new Vector3(2.339000f, -3.730000f, noodle.transform.position.z);


                Debug.Log("Processing post quest effects for noodle quest");
                break;

            case "sleep":
                var bugra = allNpcs.Find(npc => npc.npcName == "bugra");
                var egehan = allNpcs.Find(npc => npc.npcName == "egehan");

                SetPositionWithSleep(bugra.transform, -2.89f, -5.65f);
                bugra.transform.rotation = Quaternion.Euler(0, 0, 270);
                SetPositionWithSleep(egehan.transform, -1.03f, -6.48f);
                egehan.transform.rotation = Quaternion.Euler(0, 0, 270); 

                bugra.AddDialogue("Sırtım ağrıyo");
                egehan.AddDialogue("Gözlerin boşluğa dalıp giden sahipsiz bakışların");

                break;

            case "pushup":

                var oguzhan = allNpcs.Find(npc => npc.npcName == "oguzhan");
                var ugur = allNpcs.Find(npc => npc.npcName == "ugur");
                var kagan = allNpcs.Find(npc => npc.npcName == "kagan");

                SetPositionWithPushUpOffset(oguzhan.transform, 1.804f, -0.41f);
                SetPositionWithPushUpOffset(ugur.transform, -0.113f, -0.381f);
                SetPositionWithPushUpOffset(kagan.transform, 1.18f, -0.06f);

                kagan.transform.rotation = Quaternion.Euler(0, 0, 90);
                oguzhan.transform.rotation = Quaternion.Euler(0, 0, 90);
                ugur.transform.rotation = Quaternion.Euler(0, 0, 90);
                ugur.transform.localScale = new Vector3(1, -1, 1);

                oguzhan.AddDialogue("BASS HOCA BAAASSS");
                ugur.AddDialogue("Fallout, Fallout 2, Fallout 3, Fallout 4, Fallout 76... ");
                kagan.AddDialogue("Hggf... HHggfff... ");

                break;

            case "wallpaper":

                laptop.transform.position = new Vector3(0.012000f, 1.404000f, laptop.transform.position.z);


                break;

            default:
                break;
        }

    }


    private void SetPositionWithMtgOffset(Transform transform, float x, float y)
    {
        /*-0.13f
            3.85f*/
        var xOffset = -2.7f - -2.36214f;
        var yOffset = 3.32f - 3.757699f;
        var pos = transform.position;
        transform.position = new Vector3(x + xOffset, y + yOffset, pos.z);
    }
    private void SetPositionWithJustDanceOffset(Transform transform, float x, float y)
    {
        var xOffset = -0.13f - 0.2078603f;
        var yOffset = 3.85f - 4.2877f;
        var pos = transform.position;
        transform.position = new Vector3(x + xOffset, y + yOffset, pos.z);
    }
    private void SetPositionWithSleep(Transform transform, float x, float y)
    {
        var xOffset = -2.71f - -2.55214f ;
        var yOffset = -5.67f - -5.2123f ;
        var pos = transform.position;
        transform.position = new Vector3(x + xOffset , y + yOffset, pos.z);
    }
    private void SetPositionWithNoodleOffset(Transform transform, float x, float y)
    {
        var xOffset = 2.029f - 2.36686f;
        var yOffset = -3.601f - -3.163301f;
        var pos = transform.position;
        transform.position = new Vector3(x + xOffset, y + yOffset, pos.z);
    }
    private void SetPositionWithPushUpOffset(Transform transform, float x, float y)
    {
        var xOffset = 1.804f -2.14186f;
        var yOffset = -0.41f - 0.02769965f;
        
        var pos = transform.position;
        transform.position = new Vector3(x + xOffset, y + yOffset, pos.z);

    }

    private void RelocateItemNpc()
    {


    }



}
