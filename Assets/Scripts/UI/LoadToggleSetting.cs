using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class LoadToggleSetting : MonoBehaviour
{
    [SerializeField] private string playerPrefName;
    [SerializeField] private UnityEvent isOn;
    [SerializeField] private UnityEvent isOff;
    [SerializeField] private CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGame();
    }

    // Called when the object is enabled
    void OnEnable()
    {
        UpdateGame();
    }

    // Manage current state and update game
    void UpdateGame()
    {
        if (PlayerPrefs.GetInt(playerPrefName, 1) == 0)
        {
            isOff.Invoke();
        }
        else if (PlayerPrefs.GetInt(playerPrefName, 1) == 1)
        {
            isOn.Invoke();
        }
    }

    // Update amplitude gain
    public void UpdateAmplitudeGain(float value)
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = value;
    }
}
