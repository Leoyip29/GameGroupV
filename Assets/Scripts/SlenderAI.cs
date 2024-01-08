using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class SlenderAI : MonoBehaviour
{
    public Transform player; // Reference to the player's GameObject
    public float teleportDistance = 10f; // Maximum teleportation distance
    public float teleportCooldown = 5f; // Time between teleportation attempts
    public float returnCooldown = 10f; // Time before returning to the base spot
    [Range(0f, 1f)] public float chaseProbability = 0.65f; // Probability of chasing the player
    public float rotationSpeed = 5f; // Rotation speed when looking at the player
    public AudioClip teleportSound; // Reference to the teleport sound effect
    private AudioSource audioSource;

    public GameObject staticObject; // Reference to the "static" GameObject
    public float staticActivationRange = 5f; // Range at which "static" should be activated

    private Vector3 baseTeleportSpot;
    private float teleportTimer;
    private bool returningToBase;

    private NavMeshAgent navMeshAgent;
    private bool isOnGround;
    public float groundCheckDistance = 0.2f;

    public float damageRate = 10f; // Health deduction rate when player looks at the enemy
    public float playerHealth = 100f; // Initial player health
    public PlayerHealthManager playerHealthManager;

    public float maxPlayerHealth = 100f; // Maximum player health

    public float healingRate = 10f; // Healing rate when not looking at the enemy

    public GameObject restartCanvas; // Reference to the restart canvas GameObject

    private void Start()
    {
        baseTeleportSpot = transform.position;
        teleportTimer = teleportCooldown;

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the teleport sound
        audioSource.clip = teleportSound;

        // Ensure the "static" object is initially turned off
        if (staticObject != null)
        {
            staticObject.SetActive(false);
        }

        // Initialize the NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }

        navMeshAgent.stoppingDistance = 1f; // Adjust stopping distance as needed

        playerHealthManager = FindFirstObjectByType<PlayerHealthManager>();
        if (playerHealthManager == null)
        {
            Debug.LogError("PlayerHealthManager script not found!");
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        teleportTimer -= Time.deltaTime;

        if (teleportTimer <= 0f)
        {
            if (returningToBase)
            {
                TeleportToBaseSpot();
                teleportTimer = returnCooldown;
                returningToBase = false;
            }
            else
            {
                DecideTeleportAction();
                teleportTimer = teleportCooldown;
            }
        }

        if (isOnGround)
        {
            // Your AI movement logic goes here using NavMeshAgent
            navMeshAgent.SetDestination(player.position);
        }

        RotateTowardsPlayer();

        // Check player distance and toggle the "static" object accordingly
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= staticActivationRange)
        {
            if (staticObject != null && !staticObject.activeSelf)
            {
                staticObject.SetActive(true);
            }

            if (isPlayerLookingAtEnemy())
            {
                // Deduct health from the player over time
                playerHealth -= damageRate * Time.deltaTime;

                // Check if player's health is below zero
                if (playerHealth <= 0)
                {
                    // Implement player death logic (e.g., game over, respawn, etc.)
                    Debug.LogError("Player is dead");
                    ShowRestartScreen();
                }
            }
        }
        else
        {
            if (staticObject != null && staticObject.activeSelf)
            {
                staticObject.SetActive(false);
            }

            // If not looking at the enemy and outside static activation range, start healing
            playerHealth += healingRate * Time.deltaTime;

            // Clamp player's health to the maximum
            playerHealth = Mathf.Clamp(playerHealth, 0f, maxPlayerHealth);
        }
    }


    private bool isPlayerLookingAtEnemy()
    {
        // Check if the player is looking at the enemy within a certain angle
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle < 45f) // Adjust the angle as needed
        {
            // Player is looking at the enemy
            return true;
        }

        return false;
    }

    private void DecideTeleportAction()
    {
        float randomValue = Random.value;

        if (randomValue <= chaseProbability)
        {
            TeleportNearPlayer();
        }
        else
        {
            TeleportToBaseSpot();
        }
    }

    private void TeleportNearPlayer()
    {
        Vector3 randomPosition = player.position + Random.onUnitSphere * teleportDistance;
        randomPosition.y = transform.position.y; // Keep the same Y position
        transform.position = randomPosition;

        // Play the teleport sound
        audioSource.Play();
    }

    private void TeleportToBaseSpot()
    {
        transform.position = baseTeleportSpot;
        returningToBase = true;

        // Play the teleport sound
        audioSource.Play();
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f; // Ignore the vertical component

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckIfOnGround()
    {
        // Cast a ray downwards to check for the ground
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
            // If the enemy is not on the ground, stop the NavMeshAgent
            navMeshAgent.isStopped = true;
        }
    }

    private void ShowRestartScreen()
    {
        // Activate the restart canvas
        if (restartCanvas != null)
        {
            restartCanvas.SetActive(true);
        }
    }
}

