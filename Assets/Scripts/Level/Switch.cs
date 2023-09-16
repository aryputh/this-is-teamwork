using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject activeSprite;
    [SerializeField] private GameObject deactiveSprite;
    [SerializeField] private UnityEvent onActivated;

    private bool isActivated = false;

    public void ActivateSwitch()
    {
        if (!isActivated)
        {
            isActivated = true;
            activeSprite.SetActive(true);
            deactiveSprite.SetActive(false);

            onActivated.Invoke();
        }
    }
}
