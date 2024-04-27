### Map
* **[Instalation](#instalation)**
* **[Properties](#properties)**
	* [Spawner](#spawner)
		* [SpawnPoints](#spawnpoints)
		* [SpawnedObjects](#spawnedobjects)
* **[Functions](#functions)**
	* [Spawner](#spawner-1)
		* [SpawnWithCenterPosition](#spawnwithcenterposition)
		* [SpawnWithCenterPosition *(quantitatively)*](#spawnwithcenterposition-quantitatively)
		* [SpawnWithRandomPosition](#spawnwithrandomposition)
		* [SpawnWithRandomPosition *(quantitatively)*](#spawnwithrandomposition-quantitatively)
		* [SpawnInAllSpawnPointsWithCenterPosition](#spawninallspawnpointswithcenterposition)
		* [SpawnInAllSpawnPointsWithCenterPosition *(quantitatively)*](#spawninallspawnpointswithcenterposition-quantitatively)
		* [SpawnInAllSpawnPointsWithRandomPosition](#spawninallspawnpointswithrandomposition)
		* [SpawnInAllSpawnPointsWithRandomPosition *(quantitatively)*](#spawninallspawnpointswithrandomposition-quantitatively)

### Instalation
Set up **[PullSpawner v1.0.0](http://github.com/VlaKiy/PullSpawner/releases/tag/v1.0.0 "PullSpawner v1.0.0")** to your project.

### Properties
#### Spawner
##### SpawnPoints
```csharp
List<SpawnPoint> SpawnPoints
```
All SpawnPoints added to the spawner through the inspector.

------------

##### SpawnedObjects
```csharp
List<GameObject> SpawnedObjects
```
All spawned game objects.

### Functions
#### Spawner
##### SpawnWithCenterPosition
```csharp
SpawnWithCenterPosition(GameObject prefab, SpawnPoint spawnPoint);
```
- **GameObject *prefab*** - the prefab that you want to spawn.
- **SpawnPoint *spawnPoint*** - SpawnPoint where you want to spawn the prefab.

Spawning **one prefab** with **central position** in the **chosen SpawnPoint**. 

###### Example:
```csharp
[SerializeField] private Spawner _spawner;
[SerializeField] private GameObject _prefab;

private void Update()
{
	if (Input.GetKeyDown(KeyCode.Q))
		_spawner.SpawnWithCenterPosition(_prefab, _spawner.SpawnPoints[0]);
}
```
---
##### SpawnWithCenterPosition *(quantitatively)*
```csharp
SpawnWithCenterPosition(GameObject prefab, SpawnPoint spawnPoint, int n);
```
- **GameObject *prefab*** - the prefab that you want to spawn.
- **SpawnPoint *spawnPoint*** - SpawnPoint where you want to spawn the prefab.
- **int *n*** - number of prefabs to spawn.

Spawning ***n* prefabs** with **central position** in the **chosen *spawnPoint***. 

###### Example:
```csharp
[SerializeField] private Spawner _spawner;
[SerializeField] private GameObject _prefab;

private void Update()
{
	if (Input.GetKeyDown(KeyCode.Q))
		_spawner.SpawnWithCenterPosition(_prefab, _spawner.SpawnPoints[0], 3);
}
```
---
##### SpawnWithRandomPosition
```csharp
SpawnWithRandomPosition(GameObject prefab, SpawnPoint spawnPoint);
```
- **GameObject *prefab*** - the prefab that you want to spawn.
- **SpawnPoint *spawnPoint*** - SpawnPoint where you want to spawn the prefab.

Spawning ***n* prefabs** with **random position** in the **chosen *spawnPoint***. 

###### Example:
```csharp
[SerializeField] private Spawner _spawner;
[SerializeField] private GameObject _prefab;

private void Update()
{
	if (Input.GetKeyDown(KeyCode.Q))
		_spawner.SpawnWithRandomPosition(_prefab, _spawner.SpawnPoints[0]);
}
```
---
##### SpawnWithRandomPosition *(quantitatively)*
```csharp
SpawnWithRandomPosition(GameObject prefab, SpawnPoint spawnPoint, int n);
```
- **GameObject *prefab*** - the prefab that you want to spawn.
- **SpawnPoint *spawnPoint*** - SpawnPoint where you want to spawn the prefab.
- **int *n*** - number of prefabs to spawn.

Spawning ***n* prefabs** with **random position** in the **chosen *spawnPoint***. 

###### Example:
```csharp
[SerializeField] private Spawner _spawner;
[SerializeField] private GameObject _prefab;

private void Update()
{
	if (Input.GetKeyDown(KeyCode.Q))
		_spawner.SpawnWithRandomPosition(_prefab, _spawner.SpawnPoints[0], 3);
}
```
---
##### SpawnInAllSpawnPointsWithCenterPosition
```csharp
SpawnInAllSpawnPointsWithCenterPosition(GameObject prefab);
```
- **GameObject *prefab*** - the prefab that you want to spawn.

Spawning **one prefab** with **central position** in the **every SpawnPoints**. 

###### Example:
```csharp
[SerializeField] private Spawner _spawner;
[SerializeField] private GameObject _prefab;

private void Update()
{
	if (Input.GetKeyDown(KeyCode.Q))
		_spawner.SpawnInAllSpawnPointsWithCenterPosition(_prefab);
}
```
---
##### SpawnInAllSpawnPointsWithCenterPosition *(quantitatively)*
```csharp
SpawnInAllSpawnPointsWithCenterPosition(GameObject prefab, int n);
```
- **GameObject *prefab*** - the prefab that you want to spawn.
- **int *n*** - number of prefabs to spawn.

Spawning ***n* prefabs** with **central position** in the **every SpawnPoints**. 

###### Example:
```csharp
[SerializeField] private Spawner _spawner;
[SerializeField] private GameObject _prefab;

private void Update()
{
	if (Input.GetKeyDown(KeyCode.Q))
		_spawner.SpawnInAllSpawnPointsWithCenterPosition(_prefab, 3);
}
```
---
##### SpawnInAllSpawnPointsWithRandomPosition
```csharp
SpawnInAllSpawnPointsWithRandomPosition(GameObject prefab);
```
- **GameObject *prefab*** - the prefab that you want to spawn.

Spawning **one prefab** with **random position** in the **every SpawnPoints**. 

###### Example:
```csharp
[SerializeField] private Spawner _spawner;
[SerializeField] private GameObject _prefab;

private void Update()
{
	if (Input.GetKeyDown(KeyCode.Q))
		_spawner.SpawnInAllSpawnPointsWithRandomPosition(_prefab);
}
```
---
##### SpawnInAllSpawnPointsWithRandomPosition *(quantitatively)*
```csharp
SpawnInAllSpawnPointsWithRandomPosition(GameObject prefab, int n);
```
- **GameObject *prefab*** - the prefab that you want to spawn.
- **int *n*** - number of prefabs to spawn.

Spawning ***n* prefabs** with **random position** in the **every SpawnPoints**. 

###### Example:
```csharp
[SerializeField] private Spawner _spawner;
[SerializeField] private GameObject _prefab;

private void Update()
{
	if (Input.GetKeyDown(KeyCode.Q))
		_spawner.SpawnInAllSpawnPointsWithRandomPosition(_prefab, 3);
}
```
