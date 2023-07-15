using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField] private int _objectCount = 10;
    [SerializeField] private float _spawnRadius = 10f; 
    [SerializeField] private Tilemap tilemap;
    void Start()
    {
        SpawnObject(_gameObjectPrefab, _objectCount);
    }
    private void SpawnObject(GameObject _prefab, int _count)
    {
        for (int i = 0; i < _count;)
        {
            Vector3 _position = transform.position + Random.insideUnitSphere * _spawnRadius;
            _position.z = 0f;
            Collider2D[] _colliders = Physics2D.OverlapCircleAll(_position, 0.5f);
            if (_colliders.Length == 0 && tilemap.GetTile(Vector3Int.RoundToInt(_position)) != null)
            {
                Instantiate(_prefab, _position, Quaternion.identity); 
                i++; 
            }
        }
    }
}