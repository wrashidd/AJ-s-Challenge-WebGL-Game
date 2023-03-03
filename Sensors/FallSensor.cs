using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
This class registers users movement out of level.
*/
public class FallSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            GameManager.instance.HandleGameOver();
        }
    }
}
