using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlate : MonoBehaviour
{
    [SerializeField] private PlayerPlateManager plateManager;
    [Range (1, 2)] [SerializeField] private int playerNum;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player" + playerNum)) {
            if (playerNum == 1) {
                plateManager.player1PlateActive = true;
            } else {
                plateManager.player2PlateActive = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player" + playerNum)) {
            if (playerNum == 1) {
                plateManager.player1PlateActive = false;
            } else {
                plateManager.player2PlateActive = false;
            }
        }
    }
}
