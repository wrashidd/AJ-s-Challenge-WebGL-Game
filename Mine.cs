using System;

using System.Collections;
using TMPro;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject mineModel;
    [SerializeField] private GameObject mineExplosionParticle;
    [SerializeField] private GameObject innerSphere;
    [HideInInspector] public bool mineIsExploded = false;
    [SerializeField] private AudioClip _mineExplosionSoundClip;
    private AudioSource _audioSource;
    private float _startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        mineExplosionParticle.SetActive(false);
        //innerSphere.GetComponent<Transform>().GetComponent<Rigidbody>().useGravity = false;
       
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("Mine's AudioSource is NULL!");
        }
        else
        {
            _audioSource.clip = _mineExplosionSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0.05f,0f,0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player") || other.gameObject.CompareTag("gnower"))
        {
            GetComponent<Transform>().GetComponent<SphereCollider>().enabled = false;
            _audioSource.Play();
            mineIsExploded = true;
            GetComponent<Transform>().GetComponent<Animation>().enabled = false;
            mineModel.SetActive(false);
            mineExplosionParticle.SetActive(true);
           StartCoroutine(InnerSphereColorChange());
        }
    }


    IEnumerator InnerSphereColorChange()
    {
        //Change Color of the InnerSphere from black to digit green;
        yield return new WaitForSeconds(3f);
        innerSphere.GetComponent<Transform>().GetComponent<Renderer>().material.color = Color32.Lerp(new Color32((byte)0,(byte)0,(byte)0, (byte)255), new Color32((byte)0,(byte)239,(byte)85, (byte)255), (Time.time * 1 ));
        innerSphere.GetComponent<Transform>().GetComponent<BoxCollider>().enabled = true;
    }

   
}
