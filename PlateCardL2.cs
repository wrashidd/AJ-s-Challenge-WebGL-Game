using UnityEngine;

public class PlateCardL2 : MonoBehaviour
{
    private bool _isTriggered = false;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0.1f, 0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("player") && _isTriggered == false))
        {
            _isTriggered = true;
            PlateL2.plateAvailable += 1;
            Debug.Log(PlateL2.plateAvailable);
            Destroy(gameObject);
        }
    }
}
