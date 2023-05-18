using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawner : MonoBehaviour
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
    /// Spawn random prefab in spawn point.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <param name="spawnPoint"></param>
    public void Spawn(List<GameObject> prefabs, SpawnPoint spawnPoint)
    {
        if (prefabs == null)
            throw new NullReferenceException("List of Prefabs is null");

        if (!spawnPoint)
            throw new NullReferenceException("SpawnPoint is null");

        GameObject prefab = GetRandomPrefab(prefabs);

        if (!TryActivateObject(prefab, spawnPoint))
        {
            SpawnOnceObject(prefab, spawnPoint);
        }
    }

    /// <summary>
    /// Spawn random prefab in all existing spawn points.
    /// </summary>
    /// <param name="prefabs"></param>
    public void Spawn(List<GameObject> prefabs)
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
    public void Spawn(List<GameObject> prefabs, int count)
    {
        if (count <= 0)
            Debug.LogError("Count must be greater than 0");

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
    public void Spawn(List<GameObject> prefabs, SpawnPoint spawnPoint, int count)
    {
        if (count <= 0)
            Debug.LogError("Count must be greater than 0");

        for (int i = 0; i < count; i++)
        {
            Spawn(prefabs, spawnPoint);
        }
    }

    /// <summary>
    /// Return random prefab.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <returns></returns>
    public GameObject GetRandomPrefab(List<GameObject> prefabs)
    {
        int randomIndex = Random.Range(0, prefabs.Count);

        return prefabs[randomIndex];
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