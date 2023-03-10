using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for the Bar Code Gate object
*/
public class PlatformGate : MonoBehaviour
{
    [HideInInspector]
    public static bool keyIsAcquired = false;

    [SerializeField]
    private GameObject _gate;

    [SerializeField]
    private GameObject qRIcon;

    [SerializeField]
    private AudioClip _gateOpenSoundClip;

    private AudioSource _audioSource;

    private bool _hasPlayedSound = false;

    // Start is called before the first frame update
    // Preloads sound fx
    void Start()
    {
        keyIsAcquired = false;
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("PlatfromGate AudioSource is NULL!");
        }
        else
        {
            _audioSource.clip = _gateOpenSoundClip;
        }
    }

    // Plays sound fx
    // Opens the gate
    // Disables the qRIcon on the UI
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") && keyIsAcquired == true)
        {
            if (!_hasPlayedSound)
            {
                _audioSource.Play();
                _hasPlayedSound = true;
            }
            _gate.SetActive(false);
            qRIcon.SetActive(false);
        }
    }
}
