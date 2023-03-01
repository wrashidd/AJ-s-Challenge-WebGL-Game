using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
