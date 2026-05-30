using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class SampleSceneBack : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f; // Distance of the raycast
    [SerializeField] private float rayWidth = 2f; // Width of the raycast
    [SerializeField] private LayerMask playerLayer; // LayerMask to detect the player

    private void Update()
    {
        // Perform a wide, brief raycast (using BoxCast)
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(rayWidth, 0.1f), 0f, Vector2.right, rayDistance, playerLayer);

        // If the ray hits an object with the player layer
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Checkpoint reached");

            // Load the first scene (index 0) in the Build Settings
            //SceneManager.LoadScene("SampleScence2");
            Loader.Load(Loader.Scence.SampleScene2);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the box cast in the Scene view for debugging purposes
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.right * rayDistance / 2, new Vector3(rayDistance, rayWidth, 1));
    }
}
