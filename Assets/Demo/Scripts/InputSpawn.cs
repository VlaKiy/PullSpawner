using System.Collections.Generic;
using UnityEngine;

public class InputSpawn : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [Header("Spawner")]
    [SerializeField] private GameObject _prefabToSpawn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            _spawner.SpawnWithCenterPosition(_prefabToSpawn, _spawner.SpawnPoints[0]);
            
        if (Input.GetKeyDown(KeyCode.W))
            _spawner.SpawnWithCenterPosition(_prefabToSpawn, _spawner.SpawnPoints[0], 3);

        if (Input.GetKeyDown(KeyCode.E))
            _spawner.SpawnWithRandomPosition(_prefabToSpawn, _spawner.SpawnPoints[0]);

        if (Input.GetKeyDown(KeyCode.R))
            _spawner.SpawnWithRandomPosition(_prefabToSpawn, _spawner.SpawnPoints[0], 3);

        if (Input.GetKeyDown(KeyCode.T))
            _spawner.SpawnInAllSpawnPointsWithRandomPosition(_prefabToSpawn);

        if (Input.GetKeyDown(KeyCode.Y))
            _spawner.SpawnInAllSpawnPointsWithRandomPosition(_prefabToSpawn, 2);

        if (Input.GetKeyDown(KeyCode.U))
            _spawner.SpawnInAllSpawnPointsWithCenterPosition(_prefabToSpawn);

        if (Input.GetKeyDown(KeyCode.I))
            _spawner.SpawnInAllSpawnPointsWithCenterPosition(_prefabToSpawn, 3);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<GameObject> objects = _spawner.SpawnedObjects;

            foreach (var obj in objects)
            {
                obj.SetActive(false);
            }
        }
    }
}
