using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for special game entity Roman Digit X (10).
*/
public class L1X : MonoBehaviour
{
    public Rigidbody rb;

    [HideInInspector]
    public static bool innerSphereIsAcquired = false;
    private bool _hasPlayedSound = false;

    //[SerializeField] private AudioClip _digitPickUpSoundClip;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _innerSphereColorIcon;

    private void Awake()
    {
        innerSphereIsAcquired = false;
        _innerSphereColorIcon.gameObject.SetActive(false);
    }

    // On start load sound fx and change the color to black
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        GetComponent<Transform>().GetComponent<Renderer>().material.color = new Color32(
            (byte)0f,
            (byte)0f,
            (byte)0f,
            (byte)255f
        );
        GetComponent<Transform>().GetComponent<BoxCollider>().enabled = false;
    }

    // Rotate the X to 0.05f every frame in Y direction
    // and destroy the object
    // in case if the object moves upward 20 meters.
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0.05f, 0f));

        if (transform.position.y > 20f)
        {
            Destroy(gameObject);
        }
    }

    // Check AJ if whe collected the Green Paint
    // if yes change the color to green and transform upward the digit.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            if (innerSphereIsAcquired == true)
            {
                GetComponent<Renderer>().material.color = new Color32(
                    (byte)0f,
                    (byte)239f,
                    (byte)85f,
                    (byte)255f
                );

                if (!_hasPlayedSound)
                {
                    _audioSource.Play();
                    _hasPlayedSound = true;
                }

                _innerSphereColorIcon.gameObject.SetActive(false);

                rb.velocity = transform.up * 4f;

                innerSphereIsAcquired = false;
            }
            else
            {
                //Do nothing
            }
        }
    }
}
