using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform player1Pos;
    [SerializeField] private Transform player2Pos;
    [SerializeField] private int currentCheckpoint = -1;
    [SerializeField] private Checkpoint[] checkpoints;

    // Start is called before the first frame update
    void Start()
    {
        SetCheckpoint();
    }

    // When this object is enabled
    void OnEnable()
    {
        SetCheckpoint();
    }

    // Resets or sets the checkpoint according to current scene
    public void SetCheckpoint()
    {
        if (PlayerPrefs.GetInt("currentScene", 1) != SceneManager.GetActiveScene().buildIndex && checkpoints.Length != 0)
        {
            currentCheckpoint = -1;
            PlayerPrefs.SetInt("currentCheckpoint", -1);
            PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
        }
        else if (checkpoints.Length != 0)
        {
            currentCheckpoint = PlayerPrefs.GetInt("currentCheckpoint");

            for (int i = 0; i <= currentCheckpoint; i++)
            {
                checkpoints[i].CollectCheckpoint();
            }

            GoToCheckpoint();
        }
    }

    // Moves the players to the checkpoints when they are collected
    public void GoToCheckpoint() {
        if (currentCheckpoint != -1) {
            player1Pos.position = checkpoints[currentCheckpoint].GetPosition();
            player2Pos.position = checkpoints[currentCheckpoint].GetPosition();
        }
    }

    // Saves and increases to the next checkpoint
    public void SaveCheckpoint() {
        currentCheckpoint++;
        PlayerPrefs.SetInt("currentCheckpoint", currentCheckpoint);

        checkpoints[currentCheckpoint].CollectCheckpoint();
        player1Pos.position = checkpoints[currentCheckpoint].GetPosition();
        player2Pos.position = checkpoints[currentCheckpoint].GetPosition();
    }

    // Reset the checkpoint save status
    public void ResetCheckpoint()
    {
        currentCheckpoint = -1;
        PlayerPrefs.SetInt("currentCheckpoint", -1);
    }
}
