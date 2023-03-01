using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBallSeparation : MonoBehaviour
{
    [SerializeField]
    private GameObject paperBallTop;

    [SerializeField]
    private GameObject paperBallBottom;

    [SerializeField]
    private GameObject sphere;
    private bool _sphereIsGreen = false;
    private int speed = 5;
    private bool _releasePaperBallParts = false;

    private MeshRenderer _sphereRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _sphereRenderer = sphere.GetComponent<Transform>().GetComponent<MeshRenderer>();
        GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            GetComponent<SphereCollider>().enabled = false;
            _releasePaperBallParts = true;
        }

        if (other.gameObject.CompareTag("player") && _sphereIsGreen && sphere != null)
        {
            Destroy(sphere);
        }
    }

    void HandleMovement()
    {
        if (_releasePaperBallParts && paperBallTop != null && paperBallBottom != null)
        {
            paperBallTop.transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
            paperBallBottom.transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
            if (
                paperBallTop.transform.position.y >= 4f
                || paperBallBottom.transform.position.y <= -4f
            )
            {
                Destroy(paperBallTop);
                Destroy(paperBallBottom);
            }
        }

        if (_releasePaperBallParts && sphere != null)
        {
            StartCoroutine(SphereColorChange());
        }

        IEnumerator SphereColorChange()
        {
            _sphereRenderer.material.color = Color.Lerp(
                _sphereRenderer.material.color,
                new Color32((byte)0f, (byte)239f, (byte)85, (byte)255f),
                0.5f * Time.deltaTime
            );
            yield return new WaitForSeconds(3);
            GetComponent<BoxCollider>().enabled = true;
            _sphereIsGreen = true;
        }
    }
}
