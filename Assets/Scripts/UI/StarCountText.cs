using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarCountText : MonoBehaviour
{
    [SerializeField] private TMP_Text starCount;

    // Start is called before the first frame update
    void Start()
    {
        starCount.text = "" + PlayerPrefs.GetInt("starsCollected", 0);
    }

    // OnEnable is called when object is enabled
    void OnEnable()
    {
        starCount.text = "" + PlayerPrefs.GetInt("starsCollected", 0);
    }

    public void UpdateStarCountText() {
        starCount.text = "" + PlayerPrefs.GetInt("starsCollected", 0);
    }
}
