using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    void Start()
    {
        if (PlayerData.PositionSaved)
        {
            if (PlayerData.LastScene == "School")
            {
                GameObject spawnPoint = GameObject.Find("SpawnPointFromSchool");
                if (spawnPoint != null)
                {
                    transform.position = spawnPoint.transform.position;
                }
                else
                {
                    Debug.LogError("Spawn point not found in Map scene.");
                }
            }
            else
            {
                transform.position = PlayerData.LastPosition;
            }
            PlayerData.PositionSaved = false; // Reset flag
        }
    }
}

