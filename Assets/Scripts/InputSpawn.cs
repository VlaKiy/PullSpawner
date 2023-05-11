using System.Collections.Generic;
using UnityEngine;

public class InputSpawn : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject randomPrefab = _spawner.GetRandomPrefab();

            _spawner.Spawn(randomPrefab);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            List<GameObject> objects = _spawner.SpawnedObjects;

            int randomIndex = Random.Range(0, objects.Count);

            objects[randomIndex].SetActive(false);
        }
    }
}
