using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTwo : MonoBehaviour
{

    //[SerializeField] private GameObject TeleportTwoExitPosition;
    [SerializeField] private AudioClip _teleportSoundClip;
    private AudioSource _audioSource;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        _audioSource.Play();
        other.transform.position = new Vector3(12.135f, 1.92f, -9.921f);
        // other.transform.position = TeleportTwoExitPosition.gameObject.GetComponent<Transform>().position;
    }
}
