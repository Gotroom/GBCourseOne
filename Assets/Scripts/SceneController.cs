using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;


public class SceneController : MonoBehaviour
{
    #region Fields

    public static Action<bool> LoadingScene;

    [SerializeField] private GameObject _player;

    private string _sceneToLoad;
    private int _sceneToLoadIndex;

    #endregion

    #region UnityMethods

    private void Start()
    {
        WarpController.WarpToZone = OnWarpToZone;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    #endregion

    #region Methods

    public void LoadZone(int index)
    {
        _sceneToLoadIndex = index;
        StartCoroutine(LoadLeveAsyncByIndex());
    }

    public void LoadZone(string name)
    {
        _sceneToLoad = name;
        StartCoroutine(LoadLeveAsync());
    }

    private void OnWarpToZone(string name)
    {
        _sceneToLoad = name;
        StartCoroutine(LoadLeveAsync());
    }

    public Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //LoadingScene?.Invoke(false);
        Time.timeScale = 1;
        //_isGamePaused = false;
        //Paused?.Invoke(false);
    }

    IEnumerator LoadLeveAsync()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadingScene.Invoke(true);
        AsyncOperation load = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);
        while (!load.isDone)
        {
            yield return null;
        }
       // SceneManager.UnloadSceneAsync(currentScene);
    }

    IEnumerator LoadLeveAsyncByIndex()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadingScene.Invoke(true);
       // AsyncOperation load = SceneManager.LoadSceneAsync(_sceneToLoadIndex, LoadSceneMode.Additive);
        //while (!load.isDone)
        //{
            //yield return SceneManager.UnloadSceneAsync(currentScene);
        //}
            yield return SceneManager.LoadSceneAsync(_sceneToLoadIndex, LoadSceneMode.Single);

        LoadingScene.Invoke(false);
    }

    #endregion
}
