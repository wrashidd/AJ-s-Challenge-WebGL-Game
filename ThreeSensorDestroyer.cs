using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSensorDestroyer : MonoBehaviour
{

    [SerializeField] private GameObject NumberThreeSensor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
           Destroy(NumberThreeSensor); 
        }
    }
}
