using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    #region Fields

    private const string MAIN_MENU_SCENE_NAME = "MainMenu";

    [SerializeField] private SceneController _sceneController;
    [SerializeField] private bool _inGame;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _inGame = _sceneController.GetCurrentScene().name != "MainMenu";
    }

    private void Update()
    {
        _inGame = _sceneController.GetCurrentScene().name != MAIN_MENU_SCENE_NAME;
    }

    #endregion

    #region Methods

    public void LoadZone(int index)
    {
        _sceneController.LoadZone(index);
    }

    public void ExitGame()
    {
        if (_inGame)
        {
            _sceneController.LoadZone("MainMenu");
        }
        else
        {
            Application.Quit();
        }
    }

    #endregion
}
