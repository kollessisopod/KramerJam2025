using UnityEngine;

public class QuitOnEscape : MonoBehaviour
{
    private float escapeKeyHeldTime = 0f;
    public Canvas quitCanvas;
    public float holdDuration = 2f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!quitCanvas.isActiveAndEnabled)
            {
                quitCanvas.gameObject.SetActive(true);
            }
            escapeKeyHeldTime += Time.deltaTime;

            if (escapeKeyHeldTime >= holdDuration)
            {
                Debug.Log("Game exited after holding Escape for 2 seconds.");
                Application.Quit();
            }
        }
        else
        {
            if (quitCanvas.isActiveAndEnabled)
            {
                quitCanvas.gameObject.SetActive(false);
            }
            escapeKeyHeldTime = 0f; // reset if not holding
        }
    }
}