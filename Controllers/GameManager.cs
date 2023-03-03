using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/* 
This class is responsible for displaying various text messages related to a game state. 
*/
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject _gameOverMessage;

    [SerializeField]
    private GameObject _levelOneMessage;

    [SerializeField]
    private GameObject _digitsPanel;
    private Scene _scene;

    [SerializeField]
    private AudioClip _gameOverSoundClip;
    private AudioSource _audioSource;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameOverMessage.SetActive(false);
        _scene = SceneManager.GetActiveScene();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("AudioSource on the FallSensor is NULL!");
        }
        else
        {
            _audioSource.clip = _gameOverSoundClip;
        }
    }

    // Update is called once per frame
    void Update() { }

    // Calls Handle Game Over Coroutine 
    public void HandleGameOver()
    {
        StartCoroutine(GameOver());
    }

    // The game over logic that reloads the level to initial state. 
    public IEnumerator GameOver()
    {
        _levelOneMessage.SetActive(false);
        _digitsPanel.SetActive(false);
        _audioSource.Play();
        _gameOverMessage.SetActive(true);
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(_scene.name);
    }
}
