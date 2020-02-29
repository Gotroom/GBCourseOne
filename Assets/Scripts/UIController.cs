using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
using System;


public class UIController : MonoBehaviour
{
    #region Fields

    private const int MAIN_MENU_SCENE_INDEX = 0;
    public static Action<bool> Paused;

    [SerializeField] private Canvas _optionsCanvas;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _loadCanvas;
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
    }

    private void Update()
    {
        _inGame = SceneController.SceneIndex != MAIN_MENU_SCENE_INDEX;
        if (CrossPlatformInputManager.GetButtonDown("Cancel") && _inGame)
        {
            if (_isGamePaused)
            {
                UnPause();
                _mainCanvas.enabled = false;
                _optionsCanvas.enabled = false;
            }
            else
            {
                Pause();
                _mainCanvas.enabled = true;
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
            _loadCanvas.enabled = true;
            _mainCanvas.enabled = false;
            _optionsCanvas.enabled = false;
        }
        else
        {
            _loadCanvas.enabled = false;
            UnPause();
        }
    }

    public void OpenOptions()
    {
        _mainCanvas.enabled = false;
        _optionsCanvas.enabled = true;
    }

    public void CloseOptions()
    {
        _optionsCanvas.enabled = false;
        _mainCanvas.enabled = true;
    }

    #endregion
}
