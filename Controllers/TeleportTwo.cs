using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
This class is responsible for teleporting the player from lower to upper platform
*/
public class TeleportTwo : MonoBehaviour
{d
    [SerializeField]
    private AudioClip _teleportSoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    // Preloads sound fx
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.Log("TelportOne AudioSource is Null!");
        }
        else
        {
            _audioSource.clip = _teleportSoundClip;
        }
    }

    // Triggers the teleport
    // Plays sound fx
    private void OnTriggerEnter(Collider other)
    {
        _audioSource.Play();
        other.transform.position = new Vector3(12.135f, 1.92f, -9.921f);
    }
}
