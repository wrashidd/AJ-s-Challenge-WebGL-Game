using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRotLeft : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") && TeleportRotator._teleportArchIsOpen == false)
        {
            TeleportRotator._rotationLeftIsOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TeleportRotator._rotationLeftIsOn = false;
    }
}
