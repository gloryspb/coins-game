using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeletons : MonoBehaviour
{
    public GameObject prefabToSpawn; // Префаб, который мы будем спавнить
    public Transform spawnPoint; // Место, где будет спавниться префаб

    private float spawnInterval = 10f; // Интервал времени между спаунами
    private float timer = 0f; // Таймер для отслеживания времени

    private void Start()
    {
        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
    private void Update()
    {
        // Увеличиваем таймер каждый кадр
        timer += Time.deltaTime;

        // Если прошло достаточно времени, спавним префаб и сбрасываем таймер
        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }
    }

    private void SpawnPrefab()
    {
        // Создаем новый экземпляр префаба на заданной позиции
        Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
