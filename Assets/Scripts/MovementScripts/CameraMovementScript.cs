using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Transform player; // Assign in the Inspector
    public Vector3 offset = new(0, 0, -10); // Default offset (good for 2D)
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}