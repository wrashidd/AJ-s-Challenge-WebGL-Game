using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for destroying digit 3 game object
*/
public class ThreeSensorDestroyer : MonoBehaviour
{
    [SerializeField]
    private GameObject NumberThreeSensor;

    // On trigger destroys the game object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Destroy(NumberThreeSensor);
        }
    }
}
