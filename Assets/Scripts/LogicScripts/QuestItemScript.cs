using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemComponent : MonoBehaviour
{
    public string itemName = "";
    public Sprite itemSprite;
    public Quest quest;
    public bool found = false;

    public GameObject alertMarkObject;
    public SpriteRenderer alertMarkSpriteRenderer;

    // Start is called before the first frame update  
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemSprite;
        alertMarkSpriteRenderer = alertMarkObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame  
    void Update()
    {
    }

    public void Interact()
    {
        if (quest != null)
        {
            if (!found)
            {
                found = true; // Mark the item as found  
                quest.isActive = true; // Activate the quest  
                Debug.Log("Quest item found: " + itemName);

                GameManagerScript.Instance.player.GetComponent<PlayerInteraction>().Items.Add(this);
                Destroy(gameObject.GetComponent<SortingOrderByY>());
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                Destroy(gameObject.GetComponent<CircleCollider2D>());
                Destroy(gameObject.GetComponent<SpriteRenderer>());
                Destroy(gameObject.GetComponent<BoxCollider2D>());
            }
            else
            {
                Debug.Log("Item already found: " + itemName);
            }
        }
        else
        {
            Debug.LogWarning("No quest assigned to this item: " + itemName);
        }
    }

    public void ShowAlertMark()
    {
        if (alertMarkSpriteRenderer != null)
        {
            alertMarkSpriteRenderer.enabled = true; // Show the alert mark  
        }
        else
        {
            Debug.LogError("AlertMarkSpriteRenderer is not assigned in the inspector.");
        }
    }

    public void HideAlertMark()
    {
        if (alertMarkSpriteRenderer != null)
        {
            alertMarkSpriteRenderer.enabled = false; // hide the alert mark  
        }
        else
        {
            Debug.LogError("AlertMarkSpriteRenderer is not assigned in the inspector.");
        }
    }
}
