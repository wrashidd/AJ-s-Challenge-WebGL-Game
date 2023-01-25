using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnspotHider : MonoBehaviour
{
    [SerializeField] private GameObject graphics;

    private void Awake()
    {
        graphics.SetActive(false); // disable visuals of spawn point on play 
    }
}
