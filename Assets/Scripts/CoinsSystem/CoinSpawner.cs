using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bigCoinPrefab; 
    [SerializeField] private GameObject _smallCoinPrefab; 
    [SerializeField] private int _bigCoinCount = 10; 
    [SerializeField] private int _smallCoinCount = 10; 
    [SerializeField] private float _spawnRadius = 10f; 

    void Start()
    {
        SpawnCoins(_bigCoinPrefab, _bigCoinCount);
        SpawnCoins(_smallCoinPrefab, _smallCoinCount);
    }

    private void SpawnCoins(GameObject _prefab, int _count)
    {
        for (int i = 0; i < _count;)
        {
            Vector3 _position = transform.position + Random.insideUnitSphere * _spawnRadius;
            _position.z = 0f;

            Collider2D[] _colliders = Physics2D.OverlapCircleAll(_position, 0.5f);
            if (_colliders.Length == 0)
            {
                Instantiate(_prefab, _position, Quaternion.identity); 
                i++; 
            }
        }
    }
}

