using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyPickUpObject : MonoBehaviour
{
    public GameObject Instruction;//show the interact text
    public GameObject ThisTrigger;//trigger range
    public GameObject ObjectOnGround;//the object that can be collect 
    public bool Action = false;

    //intially the object and trigger range will be active
    void Start()
    {
        Instruction.SetActive(false);
        ThisTrigger.SetActive(true);
        ObjectOnGround.SetActive(true);
        

    }
    //if player collected the object, object will set to not active. Otherwise, the trigger text and action will set to active
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (ObjectOnGround.activeSelf)
            {
                Instruction.SetActive(true);
                Action = true;
            }
            else
            {
                Instruction.SetActive(false);
            }
            
        }
    }
    //if player exit the trigger range, no trigger text will show and unable to collect the item
    void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }

    //click e to collect item and trigger the counter. Also, will deactive the trigger range and text
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Action)
        {
            if (ObjectOnGround.activeSelf) // Check if the ObjectOnGround is active
            {
                ObjectOnGround.GetComponent<Item>()?.DeactivateAndCount(); // Call the method to deactivate and count
                ThisTrigger.SetActive(false);
                Instruction.SetActive(false);
                Action = false;
            }
        }
    }
}