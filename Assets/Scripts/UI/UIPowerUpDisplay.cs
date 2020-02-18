using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIPowerUpDisplay : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _image;

    #endregion

    #region UnityMethods

    void Start()
    {
        PlayerController.PowerUpApplied = OnPowerUp;
    }

    #endregion

    #region Methods

    private void OnPowerUp(bool enable)
    {
        var color = Color.white;
        color.a = enable ? 1 : 0;
        _image.color = color;
    }

    #endregion
}
