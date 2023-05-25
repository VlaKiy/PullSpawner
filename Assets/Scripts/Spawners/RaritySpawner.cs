using System;
using System.Collections.Generic;
using UnityEngine;

public class RaritySpawner : SpawnerBase
{
    /// <summary>
    /// Spawn prefab with probability in spawn point.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <param name="spawnPoint"></param>
    public override void Spawn(List<Drop> prefabs, SpawnPoint spawnPoint)
    {
        Exceptor.ThrowIfNull(prefabs, new ArgumentNullException("List of Prefabs is null"));
        Exceptor.ThrowIfNull(spawnPoint, new ArgumentNullException("SpawnPoint is null"));

        GameObject prefab = GetChancePrefab(prefabs);

        if (!TryActivateObject(prefab, spawnPoint.transform.position))
        {
            SpawnOnceObject(prefab, spawnPoint.transform.position);
        }
    }

    /// <summary>
    /// Spawn prefab with probability in all existing spawn points.
    /// </summary>
    /// <param name="prefabs"></param>
    public override void Spawn(List<Drop> prefabs)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            Spawn(prefabs, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn n prefabs with probability at all spawn points.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <param name="count"></param>
    public override void Spawn(List<Drop> prefabs, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        foreach (var spawnPoint in _spawnPoints)
        {
            Spawn(prefabs, spawnPoint, count);
        }
    }

    /// <summary>
    /// Spawn n prefabs with probability in spawn point.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <param name="spawnPoint"></param>
    /// <param name="count"></param>
    public override void Spawn(List<Drop> prefabs, SpawnPoint spawnPoint, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        for (int i = 0; i < count; i++)
        {
            Spawn(prefabs, spawnPoint);
        }
    }
}