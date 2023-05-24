using System.Collections.Generic;
using UnityEngine;

public class InputSpawn : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private RandomSpawner _randomSpawner;
    [SerializeField] private RaritySpawner _raritySpawner;

    [Header("Spawner")]
    [SerializeField] private GameObject _prefabToSpawn;

    [Header("Random Spawner")]
    [SerializeField] private List<GameObject> _prefabsToSpawn;

    [Header("Rarity Spawner")]
    [SerializeField] private List<Drop> _rarityPrefabsToSpawn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            _spawner.Spawn(_prefabToSpawn);

        if (Input.GetKeyDown(KeyCode.W))
            _spawner.Spawn(_prefabToSpawn, -4);

        if (Input.GetKeyDown(KeyCode.E))
            _spawner.Spawn(_prefabToSpawn, _spawner.SpawnPoints[2]);

        if (Input.GetKeyDown(KeyCode.R))
            _spawner.Spawn(_prefabToSpawn, _spawner.SpawnPoints[1], 0);


        if (Input.GetKeyDown(KeyCode.A))
            _randomSpawner.Spawn(_prefabsToSpawn);

        if (Input.GetKeyDown(KeyCode.S))
            _randomSpawner.Spawn(_prefabsToSpawn, 5);

        if (Input.GetKeyDown(KeyCode.D))
            _randomSpawner.Spawn(_prefabsToSpawn, _spawner.SpawnPoints[2]);

        if (Input.GetKeyDown(KeyCode.F))
            _randomSpawner.Spawn(_prefabsToSpawn, _spawner.SpawnPoints[1], 5);


        if (Input.GetKeyDown(KeyCode.Z))
            _raritySpawner.Spawn(_rarityPrefabsToSpawn);

        if (Input.GetKeyDown(KeyCode.X))
            _raritySpawner.Spawn(_rarityPrefabsToSpawn, 5);

        if (Input.GetKeyDown(KeyCode.C))
            _raritySpawner.Spawn(_rarityPrefabsToSpawn, _spawner.SpawnPoints[2]);

        if (Input.GetKeyDown(KeyCode.V))
            _raritySpawner.Spawn(_rarityPrefabsToSpawn, _spawner.SpawnPoints[1], 5);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<GameObject> objects = _spawner.SpawnedObjects;
            List<GameObject> rObjects = _randomSpawner.SpawnedObjects;

            foreach (var obj in objects)
            {
                obj.SetActive(false);
            }

            foreach (var obj in rObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}
