﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class SpawnerBase : MonoBehaviour
{
    [SerializeField] protected List<SpawnPoint> _spawnPoints;

    /// <summary>
    /// All available spawn points.
    /// </summary>
    public List<SpawnPoint> SpawnPoints => _spawnPoints;

    /// <summary>
    /// All spawned objects.
    /// </summary>
    public List<GameObject> SpawnedObjects => _spawnedObjects;

    protected Vector3 _oldSpawnPosition;
    protected List<GameObject> _spawnedObjects = new List<GameObject>();

    /// <summary>
    /// Spawn prefab in position.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <exception cref="ArgumentNullException"></exception>
    protected void Spawn(GameObject prefab, Vector3 position)
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
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    protected void Spawn(GameObject prefab, Vector3 position, int count)
    {
        Exceptor.ThrowIfTrue(count <= 0, new ArgumentOutOfRangeException("count", "Count must be greater than 0"));

        for (int i = 0; i < count; i++)
            Spawn(prefab, position);
    }

    public virtual void SpawnWithCenterPosition(GameObject prefab, SpawnPoint spawnPoint)
    {

    }

    public virtual void SpawnWithCenterPosition(GameObject prefab, SpawnPoint spawnPoint, int n)
    {

    }

    public virtual void SpawnInAllSpawnPointsWithRandomPosition(GameObject prefab)
    {
        
    }

    public virtual void SpawnInAllSpawnPointsWithCenterPosition(GameObject prefab)
    {

    }

    public virtual void SpawnInAllSpawnPointsWithCenterPosition(GameObject prefab, int count)
    {

    }

    public virtual void SpawnWithRandomPosition(GameObject prefab, SpawnPoint spawnPoint)
    {
        
    }

    public virtual void SpawnInAllSpawnPointsWithRandomPosition(GameObject prefab, int count)
    {
        
    }

    public virtual void SpawnWithRandomPosition(GameObject prefab, SpawnPoint spawnPoint, int count)
    {
        
    }

    public bool TryFindSpawnedObjectsOfType(Type type, out List<GameObject> findedObjects)
    {
        Exceptor.ThrowIfNull(type, new ArgumentNullException("Type is null"));
        Exceptor.ThrowIfNull(SpawnedObjects, new NullReferenceException("Spawned objects is null"));

        List<GameObject> objects = new List<GameObject>();

        foreach (var spawnedObject in SpawnedObjects)
        {
            if (!spawnedObject)
                continue;

            var findedObject = spawnedObject.GetComponent(type);

            if (findedObject)
                objects.Add(findedObject.gameObject);
        }

        findedObjects = objects;
        return findedObjects != null;
    }

    public bool TryFindSpawnedObjectsOfType(Type type, string childName, out List<GameObject> findedObjects)
    {
        Exceptor.ThrowIfNull(type, new ArgumentNullException("Type is null"));
        Exceptor.ThrowIfNull(childName, new ArgumentNullException("Name cannot be null"));
        Exceptor.ThrowIfNull(SpawnedObjects, new NullReferenceException("Spawned objects is null"));

        List<GameObject> objects = new List<GameObject>();

        foreach (var spawnedObject in SpawnedObjects)
        {
            if (!spawnedObject)
                continue;

            var child = spawnedObject.transform.Find(childName);

            if (!child)
                continue;

            var findedObject = child.GetComponent(type);

            if (findedObject)
                objects.Add(findedObject.gameObject);
        }

        findedObjects = objects;
        return findedObjects != null;
    }

    /// <summary>
    /// Activate object in position
    /// </summary>
    /// <param name="objPrefab"></param>
    /// <param name="position"></param>
    /// <returns>bool</returns>
    /// <exception cref="ArgumentNullException"></exception>
    protected bool TryActivateObject(GameObject objPrefab, Vector3 position)
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

    protected void SpawnOnceObject(GameObject prefab, Vector3 position)
    {
        Exceptor.ThrowIfNull(prefab, new ArgumentNullException("Prefab is null"));

        GameObject spawningObj = Instantiate(prefab, position, Quaternion.identity, transform);
        spawningObj.name = prefab.name;

        _spawnedObjects.Add(spawningObj);
    }

    protected GameObject FindNotActivatedObject(GameObject findingObject)
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

    protected Vector3 GetRandomSpawnPosition(SpawnPoint spawnPoint)
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