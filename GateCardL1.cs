using UnityEngine;

public class GateCardL1 : MonoBehaviour
{
    [SerializeField]
    private GameObject qRIcon;

    [SerializeField]
    private GameObject pedestalvase;

    [SerializeField]
    private AudioClip _cardPickedUpSoundClip;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (qRIcon != null)
        {
            qRIcon.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.Log("GateCardL1 AudioSource is Null");
        }
        else
        {
            _audioSource.clip = _cardPickedUpSoundClip;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            _audioSource.Play();
            PlatformGate.keyIsAcquired = true;
            TeleportRotator._teleportArchIsOpen = false;
            qRIcon.SetActive(true);
            Destroy(pedestalvase);
            Destroy(gameObject, 0.17f);
        }
    }
}
