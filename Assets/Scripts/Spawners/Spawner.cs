using System;
using UnityEngine;

public class Spawner : SpawnerBase
{
    /// <summary>
    /// Spawn ONCE prefab in SPAWN POINT with CENTER POSITION.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPoint"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void SpawnWithCenterPosition(GameObject prefab, SpawnPoint spawnPoint)
    {
        Exceptor.ThrowIfNull(spawnPoint, new ArgumentNullException("spawnPoint", "Spawn point is null"));

        Vector3 spawnPosition = spawnPoint.transform.position;

        Spawn(prefab, spawnPosition);
    }

    /// <summary>
    /// Spawn N prefabs in SPAWN POINT with CENTER POSITION.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPoint"></param>
    /// <param name="n"></param>
    public override void SpawnWithCenterPosition(GameObject prefab, SpawnPoint spawnPoint, int n)
    {
        Exceptor.ThrowIfTrue(n <= 0, new ArgumentOutOfRangeException("n", "Count must be greater than 0"));

        for (int i = 0; i < n; i++)
        {
            SpawnWithCenterPosition(prefab, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn ONCE prefab in SPAWN POINT with RANDOM POSITION.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPoint"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void SpawnWithRandomPosition(GameObject prefab, SpawnPoint spawnPoint)
    {
        Exceptor.ThrowIfNull(spawnPoint, new ArgumentNullException("spawnPoint", "Spawn point is null"));

        Vector3 spawnPosition = GetRandomSpawnPosition(spawnPoint);

        Spawn(prefab, spawnPosition);
    }

    /// <summary>
    /// Spawn N prefabs in SPAWN POINT with RANDOM POSITION.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPoint"></param>
    /// <param name="n"></param>
    public override void SpawnWithRandomPosition(GameObject prefab, SpawnPoint spawnPoint, int n)
    {
        Exceptor.ThrowIfTrue(n <= 0, new ArgumentOutOfRangeException("n", "Count must be greater than 0"));

        for (int i = 0; i < n; i++)
        {
            SpawnWithRandomPosition(prefab, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn ONCE prefab in ALL SPAWN POINTS with RANDOM POSITION.
    /// </summary>
    /// <param name="prefab"></param>
    public override void SpawnInAllSpawnPointsWithRandomPosition(GameObject prefab)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            SpawnWithRandomPosition(prefab, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn N prefabs in ALL SPAWN POINTS with RANDOM POSITION.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="n"></param>
    public override void SpawnInAllSpawnPointsWithRandomPosition(GameObject prefab, int n)
    {
        Exceptor.ThrowIfTrue(n <= 0, new ArgumentOutOfRangeException("n", "Count must be greater than 0"));

        foreach (var spawnPoint in _spawnPoints)
        {
            for (int i = 0; i < n; i++)
            {
                SpawnWithRandomPosition(prefab, spawnPoint);
            }
        }
    }

    /// <summary>
    /// Spawn ONCE prefab in ALL SPAWN POINTS with CENTER POSITION.
    /// </summary>
    /// <param name="prefab"></param>
    public override void SpawnInAllSpawnPointsWithCenterPosition(GameObject prefab)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            SpawnWithCenterPosition(prefab, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn N prefabs in ALL SPAWN POINTS with CENTER POSITION.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="n"></param>
    public override void SpawnInAllSpawnPointsWithCenterPosition(GameObject prefab, int n)
    {
        Exceptor.ThrowIfTrue(n <= 0, new ArgumentOutOfRangeException("n", "Count must be greater than 0"));

        foreach (var spawnPoint in _spawnPoints)
        {
            for (int i = 0; i < n; i++)
            {
                SpawnWithCenterPosition(prefab, spawnPoint);
            }
        }
    }
}