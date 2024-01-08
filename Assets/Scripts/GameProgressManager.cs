using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public int totalItemCount;
    public int itemsRemaining;

    private static GameProgressManager _instance;

    public static GameProgressManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameProgressManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("GameProgressManager");
                    _instance = go.AddComponent<GameProgressManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Initialize the item count if it's not set yet
        if (totalItemCount == 0)
        {
            totalItemCount = 3; // Set your total item count here
            itemsRemaining = totalItemCount;
        }
    }


    public void ItemCollected()
    {
        itemsRemaining--;
    }

    public void ResetItemCount()
    {
        itemsRemaining = totalItemCount;
    }
}
