using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for Rotating Teleport to the right side
*/
public class TeleportRotRight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") && TeleportRotator._teleportArchIsOpen == false)
        {
            TeleportRotator._rotationRightIsOn = true;
        }
    }

    // Triggers the left rotation to false
    private void OnTriggerExit(Collider other)
    {
        TeleportRotator._rotationRightIsOn = false;
    }
}
