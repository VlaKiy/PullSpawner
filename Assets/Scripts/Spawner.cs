using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private List<GameObject> _prefabsToSpawn;
    [SerializeField] private int _count = 2;

    private Vector3 _oldSpawnPosition;
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public List<GameObject> SpawnedObjects => _spawnedObjects;

    #region ONE TIME

    private void Start()
    {
        SpawnAllObjects();
    }

    #endregion

    public void SpawnAllObjects()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            for (int i = 0; i < _count; i++)
            {
                GameObject randomPrefab = GetRandomPrefab();

                SpawnOnceObject(randomPrefab, spawnPoint);
            }
        }
    }

    public void Spawn(GameObject prefab)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            if (!TryActivateObject(prefab, spawnPoint))
            {
                SpawnOnceObject(prefab, spawnPoint);
            }
        }
    }

    public GameObject GetRandomPrefab()
    {
        int randomIndex = Random.Range(0, _prefabsToSpawn.Count);

        return _prefabsToSpawn[randomIndex];
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
