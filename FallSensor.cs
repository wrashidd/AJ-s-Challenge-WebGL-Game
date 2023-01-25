using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSensor : MonoBehaviour
{

    //[SerializeField] private AudioClip _playerFallSoundClip;

   // private AudioSource _audioSource;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        /*_audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("AudioSource on the FallSensor is NULL!");
        }
        else
        {
            _audioSource.clip = _playerFallSoundClip;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("player"))
        {
            GameManager.instance.HandleGameOver();
           
        }
    }
}
