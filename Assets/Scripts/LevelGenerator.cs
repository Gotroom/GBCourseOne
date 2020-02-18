using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    #region Fields

    private const float PLAYER_DISTANCE_TO_SPAWN = 50.0f;
    private const float COLLIDER_OFFSET = -0.125f;
    private const float PICKUPS_SPAWN_OFFSET = 1.0f;

    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private BoxCollider2D _platformBoxCollider;
    [SerializeField] private Transform _startingPoint;
    [SerializeField] private GameObject _levelObject;
    [SerializeField] private GameObject[] _objectsOnPlatforms;

    [Header("Min and Max Position offsets")]
    [Range(0, 3)]
    [SerializeField] private float _xMinOffset = 3;
    [Range(3, 6)]
    [SerializeField] private float _xMaxOffset = 6;
    [Range(-3, 0)]
    [SerializeField] private float _yMinOffset = -3;
    [Range(0, 3)]
    [SerializeField] private float _yMaxOffset = 3;

    [Header("Min and Max platform length")]
    [SerializeField] private int _minLength = 4;
    [SerializeField] private int _maxLength = 10;

    private Vector3 _lastSpawnPoint;
    private SpriteRenderer _render;
    private int _platformPiecesLength;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        _render = _platformPrefab.GetComponent<SpriteRenderer>();
        _platformBoxCollider = _platformPrefab.GetComponent<BoxCollider2D>();
        _lastSpawnPoint = _startingPoint.Find("EndPosition").position;
        Spawn(_lastSpawnPoint);
    }

    void Update()
    {
        float sqrDistance = GetDistanceFromPlayer(_lastSpawnPoint);
        if (sqrDistance < PLAYER_DISTANCE_TO_SPAWN * PLAYER_DISTANCE_TO_SPAWN)
        {
            Spawn();
        }
        CollectGarbage();
    }

    #endregion

    #region Methods

    private void Spawn()
    {
        Transform nextPartTransformer = Spawn(_lastSpawnPoint);
        _lastSpawnPoint = nextPartTransformer.Find("EndPosition").position;
        SpawnRandomObject(Random.Range(0, _objectsOnPlatforms.Length));
    }

    private Transform Spawn(Vector3 position)
    {
        PreparePlatform();

        ResizeCollider();

        MoveEndPoint();

        return PlaceThePlatform(position);
    }

    private void PreparePlatform()
    {
        _platformPiecesLength = Random.Range(_minLength, _maxLength);
        Vector2 rendererSize = _render.size;
        rendererSize.x = _platformPiecesLength;
        _render.size = rendererSize;
    }

    private void ResizeCollider()
    {
        Vector2 colliderSize = _platformBoxCollider.size;
        Vector2 colliderOffset = _platformBoxCollider.offset;
        colliderOffset.y = COLLIDER_OFFSET;
        colliderSize.x = _platformPiecesLength;
        _platformBoxCollider.offset = colliderOffset;
        _platformBoxCollider.size = colliderSize;
    }

    private void MoveEndPoint()
    {
        Vector2 platformPosition = _platformPrefab.transform.position;
        platformPosition += _render.size / 2;
        _platformPrefab.transform.Find("EndPosition").position = platformPosition;
    }

    private Transform PlaceThePlatform(Vector3 position)
    {
        position.x += Random.Range(_xMinOffset, _xMaxOffset);
        position.y += Random.Range(_yMinOffset, _yMaxOffset);
        Transform result = Instantiate(_platformPrefab, position, Quaternion.identity).transform;
        return result;
    }

    private void CollectGarbage()
    {
        var platforms = GameObject.FindGameObjectsWithTag("Floor");
        foreach (var platform in platforms)
        {
            float sqrDistance = GetDistanceFromPlayer(platform.transform.position);
            if (sqrDistance > (PLAYER_DISTANCE_TO_SPAWN * PLAYER_DISTANCE_TO_SPAWN) * 2)
            {
                Destroy(platform);
            }
        }
    }

    private float GetDistanceFromPlayer(Vector3 position)
    {
        var playerPosition = PlayerController.PlayerInstance.transform.position;
        return (playerPosition - position).sqrMagnitude;
    }

    private void SpawnRandomObject(int type)
    {
        var spawnPoint = _lastSpawnPoint;
        spawnPoint.x -= PICKUPS_SPAWN_OFFSET;
        Instantiate(_objectsOnPlatforms[type], spawnPoint, Quaternion.identity);
    }

    #endregion
}
