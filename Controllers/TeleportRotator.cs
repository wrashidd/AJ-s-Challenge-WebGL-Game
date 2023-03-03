using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements;

/*
This class is responsible for rotating the teleport aligning
with the arch game object in the lower platform
*/
public class TeleportRotator : MonoBehaviour
{
    private float _speed = 40.0f;
    public static bool _rotationRightIsOn = false;
    public static bool _rotationLeftIsOn = false;

    [SerializeField]
    private GameObject _teleportArchBlue;

    [SerializeField]
    private GameObject _teleportArchOrange;

    [SerializeField]
    private GameObject _teleportPlatformBlue;

    [SerializeField]
    private GameObject _teleportPlatformOrange;

    [SerializeField]
    private GameObject _teleportPedestalBlue;

    [SerializeField]
    private GameObject _teleportPedestalOrange;
    private bool _teleportArchColorIsChanged = false;
    public static bool _teleportArchIsOpen;

    // Start is called before the first frame update
    // Sets Arch game object to Open
    // Freezes lower level Arch's movement
    void Start()
    {
        _teleportArchIsOpen = true;
        _rotationRightIsOn = false;
        _rotationLeftIsOn = false;
        _teleportPlatformOrange.SetActive(false);
        _teleportArchOrange.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    // Handles Rotation of the lower level Arch
    void Update()
    {
        RotatePositiveSide();
        RotateNegativeSide();
        HandleArchAccess();
    }

    void RotatePositiveSide()
    {
        if (_rotationRightIsOn)
        {
            Vector3 direction = new Vector3(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y - 90.0f,
                transform.rotation.eulerAngles.z
            );
            Quaternion targetRotation = Quaternion.Euler(direction);
            this.transform.rotation = Quaternion.RotateTowards(
                this.transform.rotation,
                targetRotation,
                Time.deltaTime * _speed
            );
        }
    }

    void RotateNegativeSide()
    {
        if (_rotationLeftIsOn)
        {
            Vector3 direction = new Vector3(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + 90.0f,
                transform.rotation.eulerAngles.z
            );
            Quaternion targetRotation = Quaternion.Euler(direction);
            this.transform.rotation = Quaternion.RotateTowards(
                this.transform.rotation,
                targetRotation,
                Time.deltaTime * _speed
            );
        }
    }

    void HandleArchAccess()
    {
        if (!_teleportArchIsOpen && _teleportArchColorIsChanged == false)
        {
            _teleportPlatformOrange.SetActive(true);
            _teleportPlatformBlue.SetActive(false);
            _teleportPedestalOrange.SetActive(true);
            _teleportPedestalBlue.SetActive(false);
            _teleportArchBlue.GetComponent<MeshRenderer>().enabled = false;
            _teleportArchOrange.GetComponent<MeshRenderer>().enabled = true;
            _teleportArchColorIsChanged = true;
        }
        else if (_teleportArchIsOpen && _teleportArchColorIsChanged)
        {
            _teleportPlatformBlue.SetActive(true);
            _teleportPlatformOrange.SetActive(false);
            _teleportPedestalBlue.SetActive(true);
            _teleportPedestalOrange.SetActive(false);
            _teleportArchOrange.GetComponent<MeshRenderer>().enabled = false;
            _teleportArchBlue.GetComponent<MeshRenderer>().enabled = true;
            _teleportArchColorIsChanged = false;
        }
    }
}
