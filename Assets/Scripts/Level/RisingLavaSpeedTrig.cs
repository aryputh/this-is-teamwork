using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLavaSpeedTrig : MonoBehaviour
{
    [SerializeField] private RisingLava lavaRising;
    [SerializeField] private float targetSpeed;
    [SerializeField] private bool enableCatchup = false; // Add a bool flag for enabling the catch-up behavior
    [SerializeField] private float catchupSpeed = 5.0f; // The speed at which the lava will catch up to the trigger

    // Change the speed of the lava when players hit the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            if (enableCatchup)
            {
                // Move the lava to the same y-level as the trigger at catchupSpeed
                lavaRising.SetConstantSpeed(catchupSpeed);
                lavaRising.MoveToYLevel(transform.position.y, catchupSpeed);
            }

            lavaRising.StartCoroutine(lavaRising.LerpToSpeed(targetSpeed));
        }
    }
}
