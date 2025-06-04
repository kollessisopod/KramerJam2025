using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PostQuestEffectsScript : MonoBehaviour
{
    public List<NpcComponent> allNpcs = new();


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
                ege.AddDialogue("Never gonna give you up");
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

            default:
                break;
        }

    }


    // More concise approach using a helper method
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
}
