using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlatformSensor : MonoBehaviour
{
    [SerializeField]
    private GameObject TeleportSecretPath;
    public static bool teleportPlatformSensorIsOn = false;

    [SerializeField]
    private AudioClip _teleportArchIsClosedSounedClip;
    private AudioSource _audioSource;

    private void Start()
    {
        teleportPlatformSensorIsOn = false;
        TeleportSecretPath.SetActive(false);
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.Log("Teleport Platform Sensor AudioSource is Null");
        }
        else
        {
            _audioSource.clip = _teleportArchIsClosedSounedClip;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (
                PlatformGate.keyIsAcquired
                && TeleportRotator._teleportArchIsOpen
                && teleportPlatformSensorIsOn == false
            )
            {
                TeleportRotator._teleportArchIsOpen = false;
                teleportPlatformSensorIsOn = true;
                TeleportSecretPath.SetActive(false);
                _audioSource.Play();
            }
        }
    }
}
