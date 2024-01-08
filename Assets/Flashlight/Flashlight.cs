using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject ON;
    public GameObject OFF;
    private bool IsON;


    // Start is called before the first frame update
    // flashlight will be off intial
    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        IsON = false;
    }

    // Update is called once per frame
    // click F to turn ON and Off object to active. Which mean to switch on and off of the light
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (IsON)
            {
                ON.SetActive(false);
                OFF.SetActive(true);
            }
            if (!IsON)
            {
                ON.SetActive(true);
                OFF.SetActive(false);
            }
            IsON =!IsON;
        }
    }
}
