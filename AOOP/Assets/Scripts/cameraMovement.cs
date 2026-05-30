using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    private Transform target; // Reference to the player's transform
    private Vector3 velocity = Vector3.zero; // Current velocity of the camera

    [Range(0, 1)]
    public float smoothTime = 0.3f; // Smoothing factor
    public Vector3 offset; // Offset from the player's position

    private void Awake()
    {
        // Find the player by tag and get its transform
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        // Calculate the target position based on the player's position and offset
        Vector3 targetPosition = target.position + offset;

        // Keep the z position fixed (e.g., -10 for 2D games)
        targetPosition.z = transform.position.z;

        // Smoothly move the camera to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
