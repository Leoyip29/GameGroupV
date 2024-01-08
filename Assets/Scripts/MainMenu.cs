using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //In main menu click play to enter the next scene
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load group chat backstory scene
    }

    //Exit the game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit the game!");
    }
}


