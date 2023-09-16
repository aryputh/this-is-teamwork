using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private TMP_Text quitButtonText;
    private int clickCount;

    void Start()
    {
        clickCount = 0;
    }

    void OnEnable()
    {
        clickCount = 0;
    }

    public void QuitTheGame()
    {
        if (clickCount == 0)
        {
            clickCount++;
            quitButtonText.text = "Confirm";
        }
        else
        {
            Application.Quit();
        }
    }
}
