using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;
using UnityEngine.Video;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;     // The entire panel
    public Image portraitImage;          // Big portrait on left
    public Image screenShotImage;
    public TMP_Text npcNameText;         // NPC name text
    public TMP_Text dialogueText;        // Dialogue content text

    public RawImage videoPortraitRawImage; // For video NPCs
    public VideoPlayer videoPlayer;
    public AudioSource videoAudioSource;

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
        portraitImage.gameObject.SetActive(true);
        videoPortraitRawImage.gameObject.SetActive(false);

        // Set NPC data
        npcNameText.text = npc.npcName;

        if(npc.name == "maliLaptop")
        {
            screenShotImage.gameObject.SetActive(true);
            portraitImage.gameObject.SetActive(false);
            screenShotImage.sprite = npc.npcPortrait;

        } else
        {

            portraitImage.gameObject.SetActive(true);
            if (npc.npcPortrait != null)
                portraitImage.sprite = npc.npcPortrait;
            else
                portraitImage.sprite = null;
        }

        dialogueText.text = dialogueLine;
    }

    public void ShowVideoDialogue(NpcSO npc, string dialogueLine)
    {
        dialoguePanel.SetActive(true);

        // UI states
        portraitImage.gameObject.SetActive(false);
        videoPortraitRawImage.gameObject.SetActive(true);

        npcNameText.text = npc.npcName;
        dialogueText.text = dialogueLine;

        // Setup video player
        videoPlayer.clip = Resources.Load<VideoClip>("Items/noodleEdit");
        if(videoPlayer.clip == null)
        {
            Debug.LogError("Video clip not found. Please ensure the video is in the Resources/Items directory.");
            return;
        }
        videoPlayer.targetTexture = new RenderTexture(848, 480, 0);
        videoPortraitRawImage.texture = videoPlayer.targetTexture;

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, videoAudioSource);

        // Before video plays
        AudioManager.Instance.PauseMusic();
        videoPlayer.Play();
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        screenShotImage.gameObject.SetActive(false);
        //stop music
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            AudioManager.Instance.ResumeMusic();
        }

    }
}