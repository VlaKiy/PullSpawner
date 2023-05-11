using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Collider _collider => GetComponent<BoxCollider>();

    public Vector3 BoundsMin => _collider.bounds.min;
    public Vector3 BoundsMax => _collider.bounds.max;
}