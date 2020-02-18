using UnityEngine;


public class DoorController : MonoBehaviour, IOpenable
{
    #region Fields

    [SerializeField] private GameObject _openableObject;
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
            _isStepped = true;
        }
    }

    #endregion

    #region IOpenable

    public void Close()
    {
        _openableObject.SetActive(true);
        _isDoorOpened = false;
    }

    public bool Open()
    {
        _openableObject.SetActive(false);
        _isDoorOpened = true;
        return true;
    }

    #endregion
}
