using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private Vector3 _oldSpawnPosition;
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    /// <summary>
    /// All available spawn points.
    /// </summary>
    public List<SpawnPoint> SpawnPoints => _spawnPoints;

    /// <summary>
    /// All spawned objects.
    /// </summary>
    public List<GameObject> SpawnedObjects => _spawnedObjects;

    /// <summary>
    /// Spawn prefab in position.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Spawn(GameObject prefab, Vector3 position)
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
    public void Spawn(GameObject prefab, Vector3 position, int count)
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
    public void Spawn(GameObject prefab, SpawnPoint spawnPoint)
    {
        Exceptor.ThrowIfNull(spawnPoint, new ArgumentNullException("spawnPoint", "Spawn point is null"));

        Vector3 spawnPosition = GetRandomSpawnPosition(spawnPoint);

        Spawn(prefab, spawnPosition);
    }

    /// <summary>
    /// Spawn prefab in all existing spawn points.
    /// </summary>
    /// <param name="prefab"></param>
    public void Spawn(GameObject prefab)
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
    public void Spawn(GameObject prefab, int count)
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
    public void Spawn(GameObject prefab, SpawnPoint spawnPoint, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        for (int i = 0; i < count; i++)
        {
            Spawn(prefab, spawnPoint);
        }
    }

    /// <summary>
    /// Activate object in position
    /// </summary>
    /// <param name="objPrefab"></param>
    /// <param name="position"></param>
    /// <returns>bool</returns>
    /// <exception cref="ArgumentNullException"></exception>
    private bool TryActivateObject(GameObject objPrefab, Vector3 position)
    {
        Exceptor.ThrowIfNull(objPrefab, new ArgumentNullException("Prefab is null"));

        GameObject notActivatedObj = FindNotActivatedObject(objPrefab);

        if (notActivatedObj)
        {
            notActivatedObj.transform.position = position;
            notActivatedObj.SetActive(true);

            return true;
        }
        else
        {
            return false;
        }
    }


    private void SpawnOnceObject(GameObject prefab, Vector3 position)
    {
        Exceptor.ThrowIfNull(prefab, new ArgumentNullException("Prefab is null"));

        GameObject spawningObj = Instantiate(prefab, position, Quaternion.identity, transform);
        spawningObj.name = prefab.name;

        _spawnedObjects.Add(spawningObj);
    }

    private GameObject FindNotActivatedObject(GameObject findingObject)
    {
        Exceptor.ThrowIfNull(findingObject, new ArgumentNullException("findingObject", "Finding Object is null"));
        Exceptor.ThrowIfNull(_spawnedObjects, new NullReferenceException("Spawned objects is null"));

        if (_spawnedObjects.Count == 0)
            return null;

        foreach (var obj in _spawnedObjects)
        {
            if (!obj)
                continue;

            if (!obj.activeInHierarchy && obj.name == findingObject.name)
            {
                return obj;
            }
        }

        return null;
    }

    private Vector3 GetRandomSpawnPosition(SpawnPoint spawnPoint)
    {
        Exceptor.ThrowIfNull(spawnPoint, new ArgumentNullException("spawnPoint", "SpawnPoint is null"));

        Vector3 spawnerBoundsMin = spawnPoint.BoundsMin;
        Vector3 spawnerBoundsMax = spawnPoint.BoundsMax;

        float randX = Random.Range(spawnerBoundsMin.x, spawnerBoundsMax.x);
        float randZ = Random.Range(spawnerBoundsMin.z, spawnerBoundsMax.z);

        Vector3 newSpawnPosition = new Vector3(randX, spawnPoint.transform.position.y, randZ);

        if (newSpawnPosition == _oldSpawnPosition)
            return GetRandomSpawnPosition(spawnPoint);

        _oldSpawnPosition = newSpawnPosition;
        return newSpawnPosition;
    }
}