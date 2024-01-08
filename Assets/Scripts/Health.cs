using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 100;

    public HealthBar healthBar;
    public GameState gameState;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameStateManager;

    [SerializeField]
    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }

    private void Start()
    {
        // Retrieve healthbar script and game state script

        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }
    // It used to define the health when take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthBar.SetHealth(currentHealth);

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // Show game over screen
    private void Die()
    {
        Debug.Log("Player has died!");

        gameState.gameOver(); // Displays game over screen
    }
}

