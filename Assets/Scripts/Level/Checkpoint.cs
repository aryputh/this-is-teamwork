using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private GameObject collectedSprite;
    [SerializeField] private GameObject uncollectedSprite;

    public void CollectCheckpoint() {
        collectedSprite.SetActive(true);
        uncollectedSprite.SetActive(false);
    }

    public Vector2 GetPosition() {
        return spawnpoint.transform.position;
    }
}
