using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*
This class is responsibe for pausing and resuming the game on user inputs
*/
public class PauseMenu : MonoBehaviour
{
    private PauseInput _pauseInput;
    public static bool _gameIsPaused = false;

    [SerializeField]
    private GameObject _pausePanel;

    private void Awake()
    {
        _pauseInput = new PauseInput();
    }

    private void Start()
    {
        _pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }

    private void DeterminePause()
    {
        if (_gameIsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Pauses the game
    public void PauseGame()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
        AudioListener.pause = true;
        _gameIsPaused = true;
    }

    // Resemes the game
    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        _gameIsPaused = false;
    }

    // Quits the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game!");
        Application.Quit();
    }

    private void OnEnable()
    {
        _pauseInput.Enable();
    }

    private void OnDisable()
    {
        _pauseInput.Disable();
    }
}
