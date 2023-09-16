using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask movingPlatformLayer;
    [SerializeField] private int movingPlatformLayerId;

    [Header("States")]
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Header("Collision")]
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    [Header("Collision")]
    [SerializeField] private CheckpointManager checkpointMan;
    [SerializeField] private GameObject playerParent;
    
    // Update is called once per frame
    void Update() {  
        onGround = Physics2D.OverlapCircle((Vector2) transform.position + bottomOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2) transform.position + bottomOffset, collisionRadius, platformLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, movingPlatformLayer);

        onWall = Physics2D.OverlapCircle((Vector2) transform.position + rightOffset, collisionRadius, groundLayer) 
            || Physics2D.OverlapCircle((Vector2) transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2) transform.position + rightOffset, collisionRadius, groundLayer);
        
        onLeftWall = Physics2D.OverlapCircle((Vector2) transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2) transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2) transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2) transform.position + leftOffset, collisionRadius);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<StarManager>() != null) {
            col.gameObject.GetComponent<StarManager>().isCollected();
        }

        if (col.gameObject.CompareTag("Checkpoint")) {
            checkpointMan.SaveCheckpoint();
        }

        if (col.gameObject.CompareTag("Switch"))
        {
            col.gameObject.SendMessage("ActivateSwitch");
        }

        if (col.gameObject.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Button"))
        {
            col.gameObject.SendMessage("ActivateButton");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Button"))
        {
            col.gameObject.SendMessage("DeactivateButton");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == movingPlatformLayerId)
        {
            gameObject.transform.SetParent(col.gameObject.transform, true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == movingPlatformLayerId)
        {
            gameObject.transform.SetParent(playerParent.transform, true);
        }
    }
}
