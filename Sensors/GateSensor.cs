using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
This class is responsible for checking if the user collected the GateCardL1.
If yes the gate should open and if not the gate should remain closed. 
*/
public class GateSensor : MonoBehaviour
{
    [SerializeField]
    private AudioClip _gateDenySoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("Gate audiosource is NULL!");
        }
        else
        {
            _audioSource.clip = _gateDenySoundClip;
        }
    }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            _audioSource.Play();
        }
    }
}
