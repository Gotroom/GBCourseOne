using UnityEngine;


public class MainMenuController : MonoBehaviour
{
    #region Fields

    private const int MAIN_MENU_SCENE_INDEX = 0;

    [SerializeField] private SceneController _sceneController;
    [SerializeField] private bool _inGame;

    #endregion

    #region UnityMethods

    private void Update()
    {
        _inGame = SceneController.SceneIndex != MAIN_MENU_SCENE_INDEX;
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
