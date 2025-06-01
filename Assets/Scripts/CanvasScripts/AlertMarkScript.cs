using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AlertMarkScript : MonoBehaviour
{
    public SpriteRenderer alertMarkSpriteRenderer;

    private void Start()
    {
        if (alertMarkSpriteRenderer == null)
        {
            Debug.LogError("AlertMarkSpriteRenderer is not assigned in the inspector.");
        }
        else
        {
            alertMarkSpriteRenderer.enabled = false; // Initially hide the alert mark
        }
    }
}
