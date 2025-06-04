using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject textPanel;     // The entire panel
    public Image image;          // Big portrait on left
    public TMP_Text text;        // Dialogue content text

    private void Awake()
    {
        HideCutscene();
    }

    private void Start()
    {

    }

    public void ShowCutscene(Image image, string stringline)
    {
        textPanel.SetActive(true);
        if (image != null)
            this.image= image;
        else
            this.image = null;

        text.text = stringline;
    }

    public void HideCutscene()
    {
        textPanel.SetActive(false);
    }
}