using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineInnerSphere : MonoBehaviour
{
    [SerializeField]
    private GameObject xDigit;
    private MeshRenderer _sphereRenderer;

    [SerializeField]
    private AudioClip _innerSpherePuckUpSoundClip;
    private AudioSource _audioSource;

    private bool _hasPlayedSound = false;

    [SerializeField]
    private GameObject _innerSphereColorIcon;

    // Start is called before the first frame update
    void Start()
    {
        _sphereRenderer = GetComponent<Transform>().GetComponent<MeshRenderer>();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("Mine's InnerSphere AudioSource is NULL!");
        }
        else
        {
            _audioSource.clip = _innerSpherePuckUpSoundClip;
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

            _innerSphereColorIcon.gameObject.SetActive(true);
            L1X.innerSphereIsAcquired = true;
            xDigit.GetComponent<BoxCollider>().enabled = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
