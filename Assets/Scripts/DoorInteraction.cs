using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public GameObject promptText; // Reference to the Text element displaying the prompt
    private bool canInteract = false;

    private void Update()
    {
        if (canInteract)
        {
            promptText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                SavePlayerPosition();
                LoadAppropriateScene();
            }
        }
        else
        {
            promptText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    private void SavePlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerData.LastPosition = player.transform.position;
            PlayerData.PositionSaved = true;
            PlayerData.LastScene = SceneManager.GetActiveScene().name; // Save the current scene name
        }
        else
        {
            Debug.LogError("Player object not found. Make sure your player is tagged correctly.");
        }
    }


    private void LoadAppropriateScene()
    {
        // Determine the current scene
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Map")
        {
            // If the current scene is "Map", load "School"
            SceneManager.LoadScene("School");
        }
        else
        {
            // Otherwise, load "Map"
            SceneManager.LoadScene("Map");
        }
    }
}

// PlayerData class to hold the player's position
public static class PlayerData
{
    public static Vector3 LastPosition;
    public static bool PositionSaved = false;
    public static string LastScene; // Add this line
}

