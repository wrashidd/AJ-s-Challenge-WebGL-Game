using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
Number three game entity behaviour 
Not currently used
*/
public class NumThreeSensor : MonoBehaviour
{
    [SerializeField]
    private GameObject numberThree;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            numberThree.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        numberThree.gameObject.SetActive(true);
    }
}
