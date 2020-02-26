using UnityEngine;
using TMPro;

public class HintController : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI _hintText;

    #endregion

    #region UnityMethods

    private void Start()
    {
        DoorController.ShowHint = OnShowHint;
        DoorController.HideHint = OnHideHint;
    }

    #endregion


    #region Methods

    void OnShowHint(string hint)
    {
        _hintText.text = hint;
        _hintText.enabled = true;
    }

    void OnHideHint()
    {
        _hintText.enabled = false;
    }

    #endregion
}
