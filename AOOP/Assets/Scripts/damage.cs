using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float _damage = 10f; // Default damage value
    [SerializeField] private float _offset = 1f; // Default offset for pushback
    [SerializeField] private float _pushDuration = 0.2f; // Duration for the smooth pushback

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the Health component from the player and apply damage if it exists
            Healt playerHealth = other.GetComponent<Healt>();
            if (playerHealth != null)
            {
                playerHealth.Taka_damage(_damage);
            }

            // Apply smooth pushback effect
            StartCoroutine(SmoothPushback(other.transform));
        }
    }

    private IEnumerator SmoothPushback(Transform playerTransform)
    {
        Vector3 startPosition = playerTransform.position;
        float pushDirection = Mathf.Sign(playerTransform.position.x - transform.position.x); // Determine direction to push the player
        Vector3 targetPosition = new Vector3(startPosition.x + pushDirection * _offset, startPosition.y, startPosition.z);

        float elapsedTime = 0f;
        while (elapsedTime < _pushDuration)
        {
            playerTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / _pushDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player reaches the target position
        playerTransform.position = targetPosition;
    }
}
