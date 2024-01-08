using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class flashlightMovement : MonoBehaviour
{
    public Animator flashlightAnim;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                flashlightAnim.ResetTrigger("Walk");
                flashlightAnim.SetTrigger("Sprint");
            }
            else
            {
                flashlightAnim.ResetTrigger("Sprint");
                flashlightAnim.SetTrigger("Walk");
            }
        }
    }
}
