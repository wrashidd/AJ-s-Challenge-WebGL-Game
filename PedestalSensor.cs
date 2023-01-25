using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalSensor : MonoBehaviour
{
    [SerializeField] private GameObject TeleportSecretPath;
    [SerializeField] private AudioClip _secretPathEnabledSoundClip;
     private AudioSource _audioSource;

    private void Start()
    {
        TeleportSecretPath.SetActive(false);
        
        _audioSource = GetComponent<AudioSource>();
       
        if (_audioSource == null)
        {
            Debug.Log("Pedestal AudioSource is Null");
        }
        else
        {
            _audioSource.clip = _secretPathEnabledSoundClip;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (PlatformGate.keyIsAcquired && TeleportRotator._teleportArchIsOpen == false)
            {
                TeleportRotator._teleportArchIsOpen = true;
                TeleportPlatformSensor.teleportPlatformSensorIsOn = false;
                _audioSource.Play();
                TeleportSecretPath.SetActive(true);
                
            }
        }
    }
}
