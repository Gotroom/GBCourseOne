using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class UIController : MonoBehaviour
{
    #region Fields

    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    public static Action<bool> Paused;

    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _loadCanvas;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private bool _inGame;

    private MainMenuController _mainMenuController;
    private OptionsController _optionsController;
    private bool _isGamePaused = false;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _optionsController = _optionsCanvas.GetComponent<OptionsController>();
        _mainMenuController = _mainCanvas.GetComponent<MainMenuController>();
        SceneController.LoadingScene = OnLoading;
        if (_sceneController.GetCurrentScene().name == MAIN_MENU_SCENE_NAME)
        {
            _inGame = false;
        }
        else
        {
            _inGame = true;
        }
    }

    private void Update()
    {
        _inGame = _sceneController.GetCurrentScene().name != MAIN_MENU_SCENE_NAME; // костыль?
        if (Input.GetButtonDown("Cancel") && _inGame)
        {
            if (_isGamePaused)
            {
                UnPause();
                _mainCanvas.SetActive(false);
                _optionsCanvas.SetActive(false);
            }
            else
            {
                Pause();
                _mainCanvas.SetActive(true);
            }
        }
    }

    #endregion

    #region Methods

    private void Pause()
    {
        Time.timeScale = 0;
        _isGamePaused = true;
        Paused?.Invoke(true);
    }

    private void UnPause()
    {
        Time.timeScale = 1;
        _isGamePaused = false;
        Paused?.Invoke(false);
    }

    private void OnLoading(bool flag)
    {
        if (flag)
        {
            Pause();
            _loadCanvas.SetActive(true);
            _mainCanvas.SetActive(false);
            _optionsCanvas.SetActive(false);
        }
        else
        {
            _loadCanvas.SetActive(false);
            UnPause();
        }
    }

    public void OpenOptions()
    {
        _mainCanvas.SetActive(false);
        _optionsCanvas.SetActive(true);
    }

    public void CloseOptions()
    {
        _optionsCanvas.SetActive(false);
        _mainCanvas.SetActive(true);
    }

    #endregion
}
