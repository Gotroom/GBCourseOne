using UnityEngine;


public class TrapController : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject[] _objectsToDestruct;

    #endregion

    #region UnityMethods

    private void Start()
    {
        foreach (var obj in _objectsToDestruct)
            obj.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (var obj in _objectsToDestruct)
                obj.SetActive(false);
        }
    }

    #endregion
}
