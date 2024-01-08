using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DiaryManager diaryManager;

    public Text counterText;
    public GameObject winText;
    public int totalItemCount; // Total Item number
    private int itemsRemaining; // The number of items remaining


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        itemsRemaining = totalItemCount;
        UpdateCounterText();
    }

    // This method will be called every time a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and assign the counterText and winText in the new scene
        counterText = GameObject.Find("Counter Text").GetComponent<Text>();
        winText = GameObject.Find("Win Screen");
        diaryManager = FindFirstObjectByType<DiaryManager>();
        // Update the counter text with the current itemsRemaining
        UpdateCounterText();

        GameObject parentObject = GameObject.Find("Game State Canvas");
        if (parentObject != null)
        {
            winText = parentObject.transform.Find("Win Screen").gameObject;
        }
        // Any other initialization for new scene
    }

    // Counter for remaining items; if items = 0 -> win
    public void ItemCollected()
    {
        itemsRemaining--;
        UpdateCounterText();

        switch (itemsRemaining)
        {
            case 1:
                diaryManager.OpenDiaryPage1();
                break;
            case 2:
                diaryManager.OpenDiaryPage2();
                break;
            case 3:
                diaryManager.OpenDiaryPage3();
                break;
        }

        CheckForWin();
    }

    public void CheckForWin()
    {
        if (itemsRemaining <= 0)
        {
            WinGame();
        }
    }

    // Update the number of items
    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = "Items Remaining: " + itemsRemaining;
        }
        else
        {
            Debug.LogError("Counter Text not assigned in the Inspector");
        }
    }

    // Show the win game screen and freeze the game
    private void WinGame()
    {
        if (winText != null)
        {
            winText.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    // Restart the game and unfreeze
    public void RestartTime()
    {
        Time.timeScale = 1f;
    }

    // If the game is frozen, click R to restart
    void Update()
    {
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        {
            RestartTime();
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
