using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GnowerController : MonoBehaviour
{
    [SerializeField] private GameObject _gnowerModel;
    private Animator _animator;
    public NavMeshAgent agent;
    
   // private float _moveSpeed = 3.25f;
    public Rigidbody rb;
    private bool _chasing;
    private float _chaseDistance = 3f; // Minimum distance at what player is detected
    private float _chaseLoseDistance = 4f; // Minimum distance between Gnower and Player to stop chasing 
    private Vector3 _targetPoint;


    [SerializeField] private AudioClip _biteSoundClip;
    private AudioSource _audioSource;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("Gnower AudioSource is NULL!");
        }
        else
        {
            _audioSource.clip = _biteSoundClip;
        }
    }


    void Update()
    {
        _targetPoint = PlayerController.instance.transform.position;
        _targetPoint.y = transform.position.y;
        
        if (!_chasing)
        {
            if (Vector3.Distance(transform.position, _targetPoint) < _chaseDistance)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                _chasing = true;
                _audioSource.Play();
                _animator.SetBool("isGnowing", true);
                
            }
        }
        else
        {
            // older chasing settings . Enable it if Nav Agent is not used
            //transform.LookAt(_targetPoint);
            //rb.velocity = transform.forward * _moveSpeed;
            agent.destination = _targetPoint;

            if (Vector3.Distance(transform.position, _targetPoint) > _chaseLoseDistance)
            {
               _audioSource.Stop();
                _animator.SetBool("isGnowing", false);
                _chasing = false;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;


            }
        }
        if(transform.position.y <-20f)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mine"))
        {
           //Destroy(gameObject); //Causes TeleportOne break. if enabled delete instructions below
           _audioSource.enabled = false;
           GetComponent<BoxCollider>().enabled = false;
           //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
           _gnowerModel.SetActive(false);

        }

        if (other.gameObject.CompareTag("player"))
        {
            GetComponent<Transform>().GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            _animator.SetBool("isGnowing", false);
            _audioSource.Stop();
        }
    }
}
