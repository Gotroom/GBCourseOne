using UnityEngine;

public class SpawnController : MonoBehaviour
{
    #region Fields

    public Transform _spawnPosition;
    public GameObject _enemy;

    #endregion

    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            for (int i = 0; i < Random.Range(1, 3); i++)
                Instantiate(_enemy, _spawnPosition.position, _spawnPosition.rotation);
            Destroy(gameObject);
        }
    }

    #endregion
}
