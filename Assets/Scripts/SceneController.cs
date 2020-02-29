using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System;


public class SceneController : MonoBehaviour
{
    #region Fields

    public static Action<bool> LoadingScene;
    public static Action StorePlayerData;
    public static int SceneIndex = 0;

    private static List<InventoryItem> _inventoryItems;
    private static int _playerHealth;

    private string _sceneToLoad;
    private int _sceneToLoadIndex;

    #endregion

    #region UnityMethods

    private void Start()
    {
        WarpController.WarpToZone = OnWarpToZone;
        DoorController.GoToScene = OnGoToScene;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    #endregion

    #region Methods

    public void LoadZone(int index)
    {
        _sceneToLoadIndex = index;
        StartCoroutine(LoadLeveAsyncByIndex(false));
    }

    public void LoadZone(string name)
    {
        _sceneToLoad = name;
        StartCoroutine(LoadLeveAsync(false));
    }

    private void OnWarpToZone(string name)
    {
        _sceneToLoad = name;
        StartCoroutine(LoadLeveAsync(true));
    }

    private void OnGoToScene(int scene)
    {
        _sceneToLoadIndex = scene;
        StartCoroutine(LoadLeveAsyncByIndex(true));
    }

    public Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
    }

    IEnumerator LoadLeveAsync(bool savePlayerData)
    {
        LoadingScene.Invoke(true);
        if (savePlayerData)
        {
            StorePlayerData.Invoke();
        }
        yield return SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);
        LoadingScene.Invoke(false);
    }

    IEnumerator LoadLeveAsyncByIndex(bool savePlayerData)
    {
        LoadingScene.Invoke(true);
        if (savePlayerData)
        {
            StorePlayerData.Invoke();
        }
        yield return SceneManager.LoadSceneAsync(_sceneToLoadIndex, LoadSceneMode.Single);
        LoadingScene.Invoke(false);
    }

    #endregion
}
