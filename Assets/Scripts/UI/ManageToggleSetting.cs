using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManageToggleSetting : MonoBehaviour
{
    [SerializeField] private string playerPrefName;
    [SerializeField] private GameObject checkedImage;
    [SerializeField] private GameObject uncheckedImage;
    [SerializeField] private UnityEvent onChecked;
    [SerializeField] private UnityEvent onUnchecked;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSprites();
    }

    // Called when the object is enabled
    void OnEnable()
    {
        UpdateSprites();
    }

    // Call checked event
    public void CheckToggle()
    {
        onChecked.Invoke();
    }

    // Call unchecked event
    public void UncheckToggle()
    {
        onUnchecked.Invoke();
    }

    // Changes the player pref value
    public void UpdatePlayerPref(int value)
    {
        PlayerPrefs.SetInt(playerPrefName, value);
    }

    // Updates the sprite based on selection
    public void UpdateSprites()
    {
        if (PlayerPrefs.GetInt(playerPrefName, 1) == 0)
        {
            checkedImage.SetActive(false);
            uncheckedImage.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(playerPrefName, 1) == 1)
        {
            checkedImage.SetActive(true);
            uncheckedImage.SetActive(false);
        }
    }
}
