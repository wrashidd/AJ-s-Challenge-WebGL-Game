using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/* 
CameraChange  class derives from MonoBehaviour 
On a user input camera changes to a different camera view
*/
public class CameraChange : MonoBehaviour
{
    public GameObject thirdCam;
    public GameObject firstCam;
    public int camMode;
    private PlayerInput _playerInput;
    private bool _CameraSwitchButtonIsPressed;

    [SerializeField]
    private GameObject _textChangeCameraViewMessage;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.CameraSwitch.performed += context => CameraChanger();
        // Debug.Log("Your Pressed C to switch the camera!");
        _textChangeCameraViewMessage.gameObject.SetActive(false);
    }

    void CameraChanger()
    {
        if (camMode == 1)
        {
            camMode = 0;
        }
        else
        {
            camMode += 1;
        }
        StartCoroutine(CamChange());
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (camMode == 0)
        {
            thirdCam.SetActive(true);
            firstCam.SetActive(false);
            _textChangeCameraViewMessage.gameObject.SetActive(false);
        }

        if (camMode == 1)
        {
            firstCam.SetActive(true);
            thirdCam.SetActive(false);
            _textChangeCameraViewMessage.gameObject.SetActive(true);
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
}
