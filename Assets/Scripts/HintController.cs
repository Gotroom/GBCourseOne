using UnityEngine;


public class HintController : MonoBehaviour
{
    #region Fields

    [HideInInspector] public string HintMessage { get { return _hintMessage; } set { _hintMessage = value; } }

    [SerializeField] private TextMesh _hintText;

    private string _hintMessage;

    #endregion

    #region UnityMethods

    private void Update()
    {
        _hintText.text = _hintMessage;
    }

    #endregion
}
