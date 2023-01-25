
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    //Movement variables
    private Animator _animator;
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _currentRunMovement;
    private float _rotationFactorPerFrame = 15.0f;
    private bool _isMovementPressed;
    private bool _isRunPressed;
    private bool _isJumpOverPressed;
    private bool _isStandingJumpPressed;
    [HideInInspector] public bool playerIsDead = false;
    [HideInInspector] public bool playerMovementIsOn = false;
    private int _isWalkingHash;
    private int _isRunningHash;
    
    /*private int _isJumpOverHash;
    private int _isStandingJumpHash;*/
    
    //Jumping variables
    private bool _isJumpPressed = false;
    private bool _isJumping = false;
    private float _initialJumpVelocity;
    private float _maxJumpHeight = 0.5f;
    private float maxJumpTime = 0.5f;
    private int _isJumpingHash;
    private bool _isJumpAnimating = false;
    private int _jumpCount = 0;
    private Dictionary<int, float> initialJumpVelocity = new Dictionary<int, float>();
    private Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
        
    //Gravity Variables
    private float _gravity = -9.8f;
    private float _groundedGravity = -0.05f;

    [SerializeField] private GameObject playerModel;
   

    [SerializeField] private AudioClip _walkStepsSoundClip;
    [SerializeField] private AudioClip _runningStepsSoundClip;
    [SerializeField] private AudioClip _JumpingSoundClip;
     private AudioSource _audioSource;
     [HideInInspector] public bool _hasHandleExitingLevelRun = false;
     private ParticleSystem _spawnFX;


     private void Awake()
     {
         //GetComponent<BoxCollider>().isTrigger = false;
         GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; // Prevents player from falling down in the first start after build
        playerModel.SetActive(false);
        playerMovementIsOn = false; 
        _hasHandleExitingLevelRun = false;
        instance = this;
        
        
        _characterController = GetComponent<CharacterController>();
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
       


        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;
        _playerInput.CharacterControls.Run.started += onRun;
        _playerInput.CharacterControls.Run.canceled += onRun;
        _playerInput.CharacterControls.JumpOver.started += onJump;
        _playerInput.CharacterControls.JumpOver.canceled += onJump;
        
        SetJumpVariables();
     }
     

    private void Start()
    {

        
        _spawnFX = GetComponentInChildren<ParticleSystem>(); 

        StartCoroutine(TurnOnPlayerModel());
        _audioSource = GetComponent<AudioSource>();
       
        
        if (_audioSource == null)
        {
            Debug.Log("AudioSource on the player is NULL!");
        }
        else
        {
           _audioSource.clip = _walkStepsSoundClip;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu._gameIsPaused) return;
        if (playerMovementIsOn) // It turns on after SpawnFX is played
        {
            HandleRotation();
            HandleAnimation();
            
            _characterController.Move(_currentMovement * Time.deltaTime);

            if (_isRunPressed)
            {
                _characterController.Move(_currentRunMovement * Time.deltaTime);
         
            }
            else
            {
                _characterController.Move(_currentMovement * Time.deltaTime);
            
            }
            
            HandleGravity();
            HandleJump();
        }

        else
        {
           HandleExitingLevel();
        }
        
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _currentRunMovement.x = _currentMovementInput.x * 3.0f;
        _currentRunMovement.z = _currentMovementInput.y * 3.0f;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        
    }

    void onRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }


    void onJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        Debug.Log("isJumpPressed");
    }

    void SetJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
      
        
        
        // Different jump animations based on time can be implemented in the future . The code is not complete need other dependencies. 
        /*float secondJumpGravity = (-2 * (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (_maxJumpHeight +2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_maxJumpHeight +4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight + 2)) / (timeToApex * 1.5f);*/

       
    }

    void HandleJump()
    {
        if (!_isJumping && _characterController.isGrounded && _isJumpPressed)
        {
            _animator.SetBool(_isJumpingHash, true);
            _isJumpAnimating = true;
            _isJumping = true;
            _currentMovement.y = _initialJumpVelocity * 0.5f;
            _currentRunMovement.y = _initialJumpVelocity * 0.5f;
           

        }
        else if (_isJumping  && _characterController.isGrounded && !_isJumpPressed)
        {
            _isJumping = false;
        }
    }
    
  
    
    void HandleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);
        
        //start walking if movement pressed is true and not already walking
        if (_isMovementPressed && !_isJumpPressed && !isWalking)
        { 
            _animator.SetBool("isWalking", true);
            _audioSource.Stop();
            _audioSource.PlayOneShot(_walkStepsSoundClip);
        }
        else if (_isMovementPressed && _isJumpPressed && !_isRunPressed && !isWalking)
        {
            _animator.SetBool("isWalking", true);
            _audioSource.Stop();
            _audioSource.PlayOneShot(_JumpingSoundClip);
            _audioSource.PlayOneShot(_walkStepsSoundClip);
            

        }
       
        else if (_isMovementPressed && _isJumpPressed && !_isRunPressed && isWalking)
        {
            _animator.SetBool("isWalking", false);
            _audioSource.Stop();
            _audioSource.PlayOneShot(_JumpingSoundClip);
           

        }
        
        //stop walking if isMovementPressed is false and not already walking
        else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool("isWalking", false);
            _audioSource.Stop();
            
           
        }
        
       
        
        
        
        // run if movement and run pressed are true and not currently walking 
        if ((_isMovementPressed && _isRunPressed ) && !isRunning)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_runningStepsSoundClip);
            _animator.SetBool(_isRunningHash, true);
            
        } 
        else if ((_isMovementPressed && _isRunPressed && _isJumpPressed) && isRunning )
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_JumpingSoundClip);
            _audioSource.PlayOneShot(_runningStepsSoundClip);
            _animator.SetBool(_isRunningHash, true);
           
        }

        
        
        
        //stop running if movement or run pressed are false and currently running
        else if ((!_isMovementPressed || !_isRunPressed ) && isRunning )
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(_walkStepsSoundClip);
            _animator.SetBool(_isRunningHash, false);
           
        }
        
       
        
        if ((!_isMovementPressed && _isRunPressed) && !isRunning)
        {
            _audioSource.Stop();
        }
        
        if (!_isMovementPressed && !_isRunPressed)
        {
            _audioSource.Stop();
        }
    }

  


    void HandleRotation()
    {
        Vector3 positionTolookAt;
        // the change in position the character should point to
        positionTolookAt.x = _currentMovement.x;
        positionTolookAt.y = 0.0f;
        positionTolookAt.z = _currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
               
            // creates a new rotation based on where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame*Time.deltaTime);
        }
    }


    void HandleGravity()
    {
        bool isFalling = _currentMovement.y <= 0.0f || !_isJumpPressed;
        float fallMultiplier = 2.0f;
        
        // apply gravity depending on if the character is grounded or not
        if (_characterController.isGrounded)
        {
            if (_isJumpAnimating)
            {
                _animator.SetBool(_isJumpingHash, false);
                _isJumpAnimating = false;
            }
          
            _currentMovement.y = _groundedGravity;
            _currentRunMovement.y = _groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = _currentMovement.y;
            float newYVelocty = _currentMovement.y + (_gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocty) * 0.5f;
            _currentMovement.y = nextYVelocity;
            _currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = _currentMovement.y;
            float newYVelocity = _currentMovement.y  + (_gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            _currentMovement.y = nextYVelocity;
            _currentRunMovement.y = nextYVelocity;
        }
    }

    void HandleExitingLevel()
    {
        if (_hasHandleExitingLevelRun)
        {
            Debug.Log("Exiting Level");
            _audioSource.Stop();
            _animator.speed = 0;
            StartCoroutine(TurnOffPlayerModel());
            _hasHandleExitingLevelRun = false;
            
        }
        
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gnower") || other.gameObject.CompareTag("mine"))
        {
            GameManager.instance.HandleGameOver();
        }
        
        if (other.gameObject.CompareTag("pushable"))
        {
            Debug.Log("Pushable Object");
            Rigidbody pushableObject = other.gameObject.GetComponent<Rigidbody>();
            if (pushableObject == null || pushableObject.isKinematic)
            {
                return;
            }

            if (other.transform.position.y <= -2f)
            {
                return;
            }

            Vector3 pushDir = new Vector3(other.transform.position.y, 0, other.transform.position.z);
            pushableObject.AddForce(pushableObject.transform.forward *2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("pushable"))
        {
           
            Rigidbody pushableObject = other.gameObject.GetComponent<Rigidbody>();
            if (pushableObject == null || pushableObject.isKinematic)
            {
                return;
            }

            if (other.transform.position.y <= -2f)
            {
                return;
            }
            Debug.Log("Pushable Object Exit");
            Vector3 pushDir = new Vector3(other.transform.position.y, 0, other.transform.position.z);
            pushableObject.AddForce(pushDir * 0f);
        }
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("pushable"))
        {
            Debug.Log("Pushable Object");
            Rigidbody pushableObject = other.gameObject.GetComponent<Rigidbody>();
            if (pushableObject == null )
            {
                return;
            }

            if (other.transform.position.y <= -2f)
            {
                return;
            }

            Vector3 pushDir = new Vector3(other.transform.position.y, 0, other.transform.position.z);
            pushableObject.MovePosition(pushDir*5f); 
        }
    }*/

    IEnumerator TurnOnPlayerModel()
    {
        yield return new WaitForSeconds(3.5f);
        playerModel.SetActive(true);
        playerMovementIsOn = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;// Very important

    }

    IEnumerator TurnOffPlayerModel()
    {
        _spawnFX.Play();
        yield return new WaitForSeconds(3f);
        playerModel.SetActive(false);
    }
}



