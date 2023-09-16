using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Called whenever object is enabled
    void OnEnable()
    {
        audioSource.Play();
    }
}
