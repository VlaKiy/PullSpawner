using System;
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

    public virtual void Spawn(GameObject prefab, Vector3 position)
    {
        
    }

    public virtual void Spawn(GameObject prefab, Vector3 position, int count)
    {
        
    }

    public virtual void Spawn(GameObject prefab)
    {
        
    }

    public virtual void Spawn(GameObject prefab, SpawnPoint spawnPoint)
    {
        
    }

    public virtual void Spawn(GameObject prefab, int count)
    {
        
    }

    public virtual void Spawn(GameObject prefab, SpawnPoint spawnPoint, int count)
    {
        
    }
    
    public virtual void Spawn(List<Drop> prefabs, SpawnPoint spawnPoint)
    {

    }

    public virtual void Spawn(List<Drop> prefabs)
    {

    }

    public virtual void Spawn(List<Drop> prefabs, int count)
    {

    }

    public virtual void Spawn(List<Drop> prefabs, SpawnPoint spawnPoint, int count)
    {
        
    }

    public virtual void Spawn(List<GameObject> prefabs, SpawnPoint spawnPoint)
    {

    }

    public virtual void Spawn(List<GameObject> prefabs)
    {

    }

    public virtual void Spawn(List<GameObject> prefabs, int count)
    {
        
    }

    public virtual void Spawn(List<GameObject> prefabs, SpawnPoint spawnPoint, int count)
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

    /// <summary>
    /// Returns a prefab that matches the probability.
    /// </summary>
    /// <param name="prefabs"></param>
    /// <returns></returns>
    protected GameObject GetChancePrefab(List<Drop> prefabs)
    {
        Exceptor.ThrowIfNull(prefabs, new NullReferenceException("Prefabs is null"));

        if (prefabs.Count == 0)
        {
            Debug.LogWarning("Prefab list is empty");
            return null;
        }

        float itemWeight = 0;
        float randomValue;

        for (int i = 0; i < prefabs.Count; i++)
        {
            itemWeight += prefabs[i].dropRarity;
        }

        randomValue = Random.Range(0, itemWeight);

        for (int i = 0; i < prefabs.Count; i++)
        {
            if (randomValue <= prefabs[i].dropRarity)
            {
                var obj = prefabs[i].objectPrefab;

                return obj;
            }

            randomValue -= prefabs[i].dropRarity;
        }

        return null;
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
}

[Serializable]
public struct Drop
{
    public string name;
    public GameObject objectPrefab;
    public int dropRarity;
}