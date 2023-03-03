using UnityEngine;

/*
This class is responsible for teleporting the player from upper to lower platform
*/
public class TeleporterOne : MonoBehaviour
{
    [SerializeField]
    private AudioClip _teleportSoundClip;

    [SerializeField]
    private GameObject TeleportOneExitPosition;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    // Preloads audio fx
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

    // Teleports the player
    // Plays teleport sound fx
    private void OnTriggerEnter(Collider other)
    {
        _audioSource.Play();
        //other.transform.position = new Vector3(-8.7f, -23.825f, 40.827f);
        other.transform.position = TeleportOneExitPosition.gameObject
            .GetComponent<Transform>()
            .position;
    }
}
