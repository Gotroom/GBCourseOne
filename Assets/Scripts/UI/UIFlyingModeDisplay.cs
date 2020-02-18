using UnityEngine;
using UnityEngine.UI;
using System;


public class UIFlyingModeDisplay : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _image;

    #endregion

    #region UnityMethods

    void Start()
    {
        PlayerController.FlyingModeApplied = OnFlyingMode;
    }

    #endregion

    #region Methods

    private void OnFlyingMode(bool enable)
    {
        var color = Color.white;
        color.a = enable ? 1 : 0;
        _image.color = color;
    }

    #endregion
}
