using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ItemsUI : MonoBehaviour
{

    public Transform iconContainer; 
    public TextMeshProUGUI jammerCountText; // Optional: Text to show the number of items
    public List<GameObject> currentIcons = new();
    public List<QuestItemComponent> questItems = new();

    public Slider volumeSlider;
    public Button muteButton;

    private void Start()
    {
        questItems = GameManagerScript.Instance.player.GetComponent<PlayerInteraction>().Items;

        // Initialize volume controls
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioManager.Instance.GetVolume();
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        if (muteButton != null)
        {
            muteButton.onClick.AddListener(ToggleMute);
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        jammerCountText.text = "Jammer Sayısı : " + GameManagerScript.Instance.jammerCount.ToString();

        // Clear previous icons
        foreach (GameObject icon in currentIcons)
        {
            Destroy(icon);
        }
        currentIcons.Clear();

        // Guard clause
        if (iconContainer == null || questItems == null || questItems.Count == 0) return;

        // Calculate icon size based on container width
        float containerWidth = iconContainer.GetComponent<RectTransform>().rect.width;
        int itemCount = questItems.Count;
        float padding = 2f;
        float totalPadding = padding * (itemCount - 1);
        float iconWidth = (containerWidth - 20f) ;

        foreach (QuestItemComponent item in questItems)
        {
            if (item.itemSprite == null) continue;

            GameObject iconGO = new GameObject("ItemIcon", typeof(RectTransform), typeof(Image));
            iconGO.transform.SetParent(iconContainer, false);

            Image image = iconGO.GetComponent<Image>();
            image.sprite = item.itemSprite;
            image.preserveAspect = true;

            RectTransform rt = iconGO.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0.8f);
            rt.anchorMax = new Vector2(0, 0.8f);
            rt.pivot = new Vector2(0, 0.8f);
            rt.sizeDelta = new Vector2(iconWidth, iconWidth); // Square icons

            currentIcons.Add(iconGO);
        }
    }


    private void OnVolumeChanged(float value)
    {
        AudioManager.Instance.SetVolume(value);
    }

    private void ToggleMute()
    {
        AudioManager.Instance.ToggleMute();
        volumeSlider.value = AudioManager.Instance.GetVolume();
    }


}
