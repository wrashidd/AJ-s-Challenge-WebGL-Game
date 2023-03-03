using UnityEngine;

/*
This class is responsible for activating level 2 floor tiles
Work in progress
*/
public class PlateCardL2 : MonoBehaviour
{
    private bool _isTriggered = false;

    // Update is called once per frame
    // Rotate the card in Y direction
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0.1f, 0f));
    }

    //Activates the card
    //Removes the card from the game on touch
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("player") && _isTriggered == false))
        {
            _isTriggered = true;
            PlateL2.plateAvailable += 1;
            Destroy(gameObject);
        }
    }
}
