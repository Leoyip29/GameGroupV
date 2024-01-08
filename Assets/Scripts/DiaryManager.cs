using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    public GameObject diaryPage1;
    public GameObject diaryPage2;
    public GameObject diaryPage3;

    public GameManager gameManager;

    private void Awake()
    {
        // Find the GameManager instance when this object is initialized
        gameManager = GameManager.instance;

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }

    private void Update()
    {
        // Check if 'C' key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseAllDiaryPages();
        }
    }
    public void OpenDiaryPage1()
    {
        diaryPage1.SetActive(true);
    }

    public void CloseDiaryPage1()
    {
        diaryPage1.SetActive(false);
    }

    public void OpenDiaryPage2()
    {
        diaryPage2.SetActive(true);
    }

    public void CloseDiaryPage2()
    {
        diaryPage2.SetActive(false);
    }

    public void OpenDiaryPage3()
    {
        diaryPage3.SetActive(true);
    }

    public void CloseDiaryPage3()
    {
        diaryPage3.SetActive(false);
        if (gameManager != null)
        {
            gameManager.CheckForWin();
        }
        else
        {
            Debug.LogError("GameManager is null when trying to call CheckForWin");
        }
    }

    // Helper method to close all diary pages
    private void CloseAllDiaryPages()
    {
        CloseDiaryPage1();
        CloseDiaryPage2();
        CloseDiaryPage3();
    }

}
