using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy prefab")]
    public GameObject EnemyPrefab;

    [Header("amount of Enemies")]
    public int spawnAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(EnemyPrefab);
        EnemySpawn();
    }

    void EnemySpawn()
    {
        for (int i = 1; i < spawnAmount; i++)
        {
            Instantiate(EnemyPrefab);
        }
    }
}

