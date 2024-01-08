using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{

    public Transform mainCamera;

    // Turns enemy health bar towards camera
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
