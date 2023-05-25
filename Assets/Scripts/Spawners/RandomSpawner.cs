using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : SpawnerBase
{
    /// <summary>
    /// Spawn random prefab in spawn point.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <param name="spawnPoint"></param>
    public override void Spawn(List<GameObject> prefabs, SpawnPoint spawnPoint)
    {
        Exceptor.ThrowIfNull(prefabs, new NullReferenceException("List of Prefabs is null"));
        Exceptor.ThrowIfNull(spawnPoint, new NullReferenceException("SpawnPoint is null"));

        GameObject prefab = GetRandomPrefab(prefabs);

        if (!TryActivateObject(prefab, spawnPoint.transform.position))
        {
            SpawnOnceObject(prefab, spawnPoint.transform.position);
        }
    }

    /// <summary>
    /// Spawn random prefab in all existing spawn points.
    /// </summary>
    /// <param name="prefabs"></param>
    public override void Spawn(List<GameObject> prefabs)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            Spawn(prefabs, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn n random prefabs at all spawn points.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <param name="count"></param>
    public override void Spawn(List<GameObject> prefabs, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        foreach (var spawnPoint in _spawnPoints)
        {
            for (int i = 0; i < count; i++)
            {
                Spawn(prefabs, spawnPoint);
            }
        }
    }

    /// <summary>
    /// Spawn n random prefabs in spawn point.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <param name="spawnPoint"></param>
    /// <param name="count"></param>
    public override void Spawn(List<GameObject> prefabs, SpawnPoint spawnPoint, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        for (int i = 0; i < count; i++)
        {
            Spawn(prefabs, spawnPoint);
        }
    }
}