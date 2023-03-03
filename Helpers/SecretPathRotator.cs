using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
This class is responsible for rotating Secret path game object
*/
public class SecretPathRotator : MonoBehaviour
{
    // On start set Secret Path random direction
    private void Start()
    {
        Vector3 euler = transform.eulerAngles;
        euler.y = Random.Range(0f, 360f);
        transform.eulerAngles = euler;
    }
}
