using System;
using UnityEngine;

public class Spawner : SpawnerBase
{
    /// <summary>
    /// Spawn prefab in position.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Spawn(GameObject prefab, Vector3 position)
    {
        Exceptor.ThrowIfNull(prefab, new ArgumentNullException("Prefab is null"));

        if (!TryActivateObject(prefab, position))
        {
            SpawnOnceObject(prefab, position);
        }
    }

    /// <summary>
    /// Spawn n prefabs in position.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="count"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Spawn(GameObject prefab, Vector3 position, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        for (int i = 0; i < count; i++)
            Spawn(prefab, position);
    }

    /// <summary>
    /// Spawn prefab in spawn point.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPoint"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public override void Spawn(GameObject prefab, SpawnPoint spawnPoint)
    {
        Exceptor.ThrowIfNull(spawnPoint, new ArgumentNullException("spawnPoint", "Spawn point is null"));

        Vector3 spawnPosition = GetRandomSpawnPosition(spawnPoint);

        Spawn(prefab, spawnPosition);
    }

    /// <summary>
    /// Spawn prefab in all existing spawn points.
    /// </summary>
    /// <param name="prefab"></param>
    public override void Spawn(GameObject prefab)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            Spawn(prefab, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn n prefabs at all spawn points.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="count"></param>
    public override void Spawn(GameObject prefab, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        foreach (var spawnPoint in _spawnPoints)
        {
            for (int i = 0; i < count; i++)
            {
                Spawn(prefab, spawnPoint);
            }
        }
    }

    /// <summary>
    /// Spawn n prefabs in spawn point.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPoint"></param>
    /// <param name="count"></param>
    public override void Spawn(GameObject prefab, SpawnPoint spawnPoint, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        for (int i = 0; i < count; i++)
        {
            Spawn(prefab, spawnPoint);
        }
    }
}