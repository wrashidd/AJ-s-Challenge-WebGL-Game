using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for hiding Spawn Spot FX
*/
public class SpawnspotHider : MonoBehaviour
{
    [SerializeField]
    private GameObject graphics;

    private void Awake()
    {
        graphics.SetActive(false); // disable visuals of spawn point on play
    }
}
