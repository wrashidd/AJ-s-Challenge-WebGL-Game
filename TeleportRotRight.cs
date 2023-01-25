using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRotRight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") && TeleportRotator._teleportArchIsOpen == false)
        {
            TeleportRotator._rotationRightIsOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TeleportRotator._rotationRightIsOn = false;
    }

}
