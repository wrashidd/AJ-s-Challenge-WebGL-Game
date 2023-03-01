using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVX : MonoBehaviour
{
    public Rigidbody rb;

    private bool _hasPlayedSound = false;

    //[SerializeField] private AudioClip _digitPickUpSoundClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0.05f, 0f));

        if (transform.position.y > 20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            if (!_hasPlayedSound)
            {
                _audioSource.Play();
                _hasPlayedSound = true;
            }

            rb.velocity = transform.up * 4f;
        }
    }
}
