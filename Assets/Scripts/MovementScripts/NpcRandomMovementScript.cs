using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCRandomMovementScript : MonoBehaviour
{

    public float speed = 2.5f;
    public Rigidbody2D rb;
    public float moveRadius = 0.4f; // The radius within which the NPC can move randomly
    public Common common;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        common = gameObject.AddComponent<Common>();
        StartCoroutine(MoveRandomly());

    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        common.FixPhysics(rb);
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            Vector2 randomOffset = Random.insideUnitCircle * moveRadius;
            Vector2 targetPosition = rb.position + randomOffset;

            float elapsedTime = 0f;
            float journeyTime = Vector2.Distance(rb.position, targetPosition) / speed;

            Vector2 startPosition = rb.position;

            while (elapsedTime < journeyTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / journeyTime;

                Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, t);
                rb.MovePosition(newPosition);

                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Reverse direction upon collision with an obstacle
            Vector3 movement = new Vector3(-rb.velocity.x, -rb.velocity.y, 0).normalized;
            rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Stop movement upon collision with the player
            rb.velocity = Vector2.zero;
            Debug.Log("NPC stopped moving due to collision with player.");
        }
    }
}
