using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        //_pauseInput.Pause.PauseGame.performed += _ => DeterminePause(); // Disabled for WebGl
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
        AudioListener.pause = true;
        _gameIsPaused = true;
    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        _gameIsPaused = false;
    }

    public void LoadMenu()
    {
        //Time.timeScale = 1f;  // Enable it when Main Menu Scene is created
        Debug.Log("Loading Menue");
    }

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
