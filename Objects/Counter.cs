using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 
Counter derives form main MonoBehaviour class
This class implements logic of notifying users about the digit object were collected. 
*/
public class Counter : MonoBehaviour
{
    private Scene scene;

    [SerializeField]
    private TMP_Text digitI,
        digitV,
        digitX;

    [SerializeField]
    private GameObject _LevelCompletionSign;

    private int _LevelSum = 0;

    [SerializeField]
    private AudioClip _levelCompletionSoundClip;
    private AudioSource _audioSource;

    private bool _hasPlayedSound = false;

    private bool _hasRunCode = false;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        _LevelCompletionSign.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.Log("Counter AudioSource is NULL!");
        }
        else
        {
            _audioSource.clip = _levelCompletionSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LevelSumCounter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("digitI"))
        {
            digitI.color = Color.green;
            _LevelSum += 1;
        }
        if (other.CompareTag("digitV"))
        {
            digitV.color = Color.green;
            _LevelSum += 1;
        }
        if (other.CompareTag("digitX"))
        {
            digitX.color = Color.green;
            _LevelSum += 1;
        }
    }

    private void LevelSumCounter()
    {
        if (_LevelSum == 3 && !_hasRunCode)
        {
            _LevelCompletionSign.gameObject.SetActive(true);
            PlayerController.instance.playerMovementIsOn = false;
            PlayerController.instance._hasHandleExitingLevelRun = true;
            if (!_hasPlayedSound)
            {
                _audioSource.Play();
                _hasPlayedSound = true;
            }
            //PlayerController.instance.deathRegister = false;
            StartCoroutine(Level1Reload());
            _hasRunCode = true;
        }
    }

    IEnumerator Level1Reload()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(scene.name);
    }
}
