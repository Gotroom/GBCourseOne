using UnityEngine;
using System;


public class DoorController : MonoBehaviour, IOpenable
{
    #region Fields

    private const string USE_HINT = "Press \"E\"";
    private const string KEY_NEEDED_HINT = "Door is closed. Grab the key!";
    private const string OPENNED_HINT = "Door is opened. Press \"Use\" button to enter next zone!";

    public static Action<string> ShowHint;
    public static Action HideHint;
    public static Func<bool> CheckKey;

    private bool _isStepped = false;
    private bool _isDoorOpened = false;

    #endregion

    #region UnityMethods

    private void Update()
    {
        if (_isStepped && Input.GetButtonDown("Use"))
        {
            if (_isDoorOpened)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowHint.Invoke(USE_HINT);
            _isStepped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideHint.Invoke();
            _isStepped = false;
        }
    }

    #endregion

    #region IOpenable

    public void Close()
    {
        _isDoorOpened = false;
    }

    public bool Open()
    {
        if (!CheckKey.Invoke())
        {
            ShowHint.Invoke(KEY_NEEDED_HINT);
            _isDoorOpened = false;
        }
        else
        {
            ShowHint.Invoke(OPENNED_HINT);
            _isDoorOpened = true;
        }
        return _isDoorOpened;
    }

    #endregion
}
