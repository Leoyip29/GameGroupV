using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public int damage = 10;

    private void Start()
    {
        // Attempt to automatically find and assign the player using the tag "Player"
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Player found and assigned!");
            }
            else
            {
                Debug.LogError("Player not found, please assign it manually in the inspector!");
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        Debug.Log("Following the player!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Damaging the player!");
            }
            else
            {
                Debug.LogError("PlayerHealth script not found on the player object!");
            }
        }
    }
}
    