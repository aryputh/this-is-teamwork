using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBorderManager : MonoBehaviour
{
    [SerializeField] private GameObject topSpike;
    [SerializeField] private GameObject bottomSpike;
    [SerializeField] private GameObject leftSpike;
    [SerializeField] private GameObject rightSpike;

    void Start()
    {
        StandaloneAddCollider();
    }
    
    void StandaloneAddCollider()
    {
        // Check for an active camera
        if (Camera.main == null)
        {
            Debug.LogError("Camera.main not found, failed to create edge colliders");
            return;
        }

        // Make sure camera is orthographic
        var cam = Camera.main;
        if (!cam.orthographic)
        {
            Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders");
            return;
        }

        // Get the position of each edge
        Vector2 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);
        Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

        // Get the camera's current position
        Vector2 camPos = cam.transform.position;

        // Add or use existing EdgeCollider2D
        var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

        // Set the edge points in the collider
        var edgePoints = new[] { bottomLeft - camPos, topLeft - camPos, topRight - camPos, bottomRight - camPos, bottomLeft - camPos };
        edge.points = edgePoints;

        // Set the spike positions
        topSpike.transform.position = new Vector2(camPos.x, topLeft.y);
        bottomSpike.transform.position = new Vector2(camPos.x, bottomLeft.y);
        leftSpike.transform.position = new Vector2(topLeft.x, camPos.y);
        rightSpike.transform.position = new Vector2(topRight.x, camPos.y);

        // Make the edges of the camera make the level restart
        edge.isTrigger = true;
        edge.gameObject.tag = "Death";
    }
}
