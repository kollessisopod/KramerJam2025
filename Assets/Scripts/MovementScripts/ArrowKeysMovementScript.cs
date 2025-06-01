using UnityEngine;
public class ArrowKeysMovementScript : MonoBehaviour
{

    public float speed = 5.0f;
    public Rigidbody2D rb;
    public Common common;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        common = gameObject.AddComponent<Common>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(moveHorizontal, moveVertical, 0) * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        common.FixPhysics(rb);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Stop movement upon collision with the player
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Npc"))
        {
            // Stop movement upon collision with the player
            rb.velocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop movement upon collision with the player
            rb.velocity = Vector2.zero;
        }
    }
}
