using UnityEngine;

/* 
This class is responsible for GateCard L2 that opens the gate in upper platform. 
*/
public class GateCardL2 : MonoBehaviour
{
    [SerializeField]
    private GameObject qRIcon;

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

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            _audioSource.Play();
            PlatformGate.keyIsAcquired = true;
            qRIcon.SetActive(true);
            Destroy(gameObject, 0.17f);
        }
    }
}
