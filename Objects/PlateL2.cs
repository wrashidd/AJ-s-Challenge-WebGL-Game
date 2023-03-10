using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for floor tiles
Work in progress
*/
public class PlateL2 : MonoBehaviour
{
    [SerializeField]
    private GameObject _plateMesh;

    [SerializeField]
    private GameObject _plateSphere;
    public static int plateAvailable = 0;

    // Start is called before the first frame update
    void Start()
    {
        _plateMesh.SetActive(false);
        plateAvailable = 0;
    }

    // Update is called once per frame
    void Update() { }

    // Triggers the plates behavior
    // Activates and disables colliders on the plate
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            if (plateAvailable > 0)
            {
                _plateSphere.GetComponent<Transform>().gameObject.SetActive(false);
                plateAvailable -= 1;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                _plateMesh.SetActive(true); 
            }
            else if (plateAvailable <= 0)
            {
                Debug.Log("Plate Available is 0");
            }
        }
    }
}
