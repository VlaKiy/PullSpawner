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
    /// Spawn prefab in spawn point.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPoint"></param>
    public void Spawn(GameObject prefab, SpawnPoint spawnPoint)
    {
        if (prefab == null)
            throw new NullReferenceException("List of Prefabs is null");

        if (!spawnPoint)
            throw new NullReferenceException("SpawnPoint is null");

        if (!TryActivateObject(prefab, spawnPoint))
        {
            SpawnOnceObject(prefab, spawnPoint);
        }
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
        if (count <= 0)
            Debug.LogError("Count must be greater than 0");

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
    /// <param name="prefabs"></param>
    /// <param name="spawnPoint"></param>
    /// <param name="count"></param>
    public void Spawn(GameObject prefab, SpawnPoint spawnPoint, int count)
    {
        if (count <= 0)
            Debug.LogError("Count must be greater than 0");

        for (int i = 0; i < count; i++)
        {
            Spawn(prefab, spawnPoint);
        }
    }

    private bool TryActivateObject(GameObject objPrefab, SpawnPoint spawnPoint)
    {
        if (!objPrefab)
            throw new NullReferenceException("Object prefab is null");

        if (!spawnPoint)
            throw new NullReferenceException("SpawnPoint is null");

        GameObject notActivatedObj = FindNotActivatedObject(objPrefab);

        if (notActivatedObj)
        {
            notActivatedObj.transform.position = GetRandomSpawnPosition(spawnPoint);
            notActivatedObj.SetActive(true);

            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpawnOnceObject(GameObject prefab, SpawnPoint spawnPoint)
    {
        if (!prefab)
            throw new NullReferenceException("Prefab is null");

        if (!spawnPoint)
            throw new NullReferenceException("SpawnPoint is null");

        Vector3 spawnPosition = GetRandomSpawnPosition(spawnPoint);

        GameObject spawningObj = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
        spawningObj.name = prefab.name;

        _spawnedObjects.Add(spawningObj);
    }

    private GameObject FindNotActivatedObject(GameObject findingObject)
    {
        if (!findingObject)
            throw new NullReferenceException("Example Object is null");

        if (_spawnedObjects == null)
            throw new NullReferenceException("Spawned objects is null");

        if (_spawnedObjects.Count == 0)
            return null;

        foreach (var obj in _spawnedObjects)
        {
            if (!obj)
                throw new NullReferenceException("Object in list spawnedObjects is null");

            if (!obj.activeInHierarchy && obj.name == findingObject.name)
            {
                return obj;
            }
        }

        return null;
    }

    private Vector3 GetRandomSpawnPosition(SpawnPoint spawnPoint)
    {
        if (!spawnPoint)
            throw new NullReferenceException("SpawnPoint is null");

        Vector3 spawnerBoundsMin = spawnPoint.BoundsMin;
        Vector3 spawnerBoundsMax = spawnPoint.BoundsMax;

        float randX = Random.Range(spawnerBoundsMin.x, spawnerBoundsMax.x);
        float randZ = Random.Range(spawnerBoundsMin.z, spawnerBoundsMax.z);

        Vector3 newSpawnPosition = new Vector3(randX, 0f, randZ);

        if (newSpawnPosition == _oldSpawnPosition)
            return GetRandomSpawnPosition(spawnPoint);

        _oldSpawnPosition = newSpawnPosition;
        return newSpawnPosition;
    }
}