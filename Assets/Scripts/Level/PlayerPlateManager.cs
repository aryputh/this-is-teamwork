using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPlateManager : MonoBehaviour
{
    public bool player1PlateActive;
    public bool player2PlateActive;
    [SerializeField] private UnityEvent onBothPlayersDetected;

    // Update is called once per frame
    void Update()
    {
        if (player1PlateActive && player2PlateActive) {
            player1PlateActive = false;
            player2PlateActive = false;

            onBothPlayersDetected.Invoke();
        }
    }
}
