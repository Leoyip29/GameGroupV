using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum player health
    private float currentHealth; // Current player health

    public Slider healthBar; // Reference to the health bar UI slider

    public float restartDelay = 3f; // Time delay before restarting the game
    private bool isPlayerDead = false; // Flag to check if the player is dead

    public GameObject restartCanvas;


    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            // Set the maximum value for the health bar
            healthBar.maxValue = maxHealth;
            UpdateHealthBar();
        }
        else
        {
            Debug.LogError("Health bar reference is missing!");
        }
    }

    private void Update()
    {
        // Check for player death
        if (currentHealth <= 0f && !isPlayerDead)
        {
            // Player is dead, initiate restart
            isPlayerDead = true;
            ShowRestartScreen();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        // Deduct health when the player takes damage
        currentHealth -= damageAmount;

        // Update the health bar UI
        UpdateHealthBar();
    }

    public void Heal(float amount)
    {
        // Increase player health when healing
        currentHealth += amount;

        // Clamp current health to the maximum
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Update the health bar UI
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Update the health bar value
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
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
