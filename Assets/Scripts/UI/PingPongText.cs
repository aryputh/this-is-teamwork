using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PingPongText : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField] [Range(0, 1)] private float minAlpha;
    [SerializeField] private float animSpeed;

    // Update is called once per frame
    void Update()
    {
        targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, minAlpha + Mathf.PingPong(Time.time * animSpeed, 1 - minAlpha));
    }
}
