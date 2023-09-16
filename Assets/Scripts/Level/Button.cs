using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject activeSprite;
    [SerializeField] private GameObject deactiveSprite;
    [SerializeField] private UnityEvent onActivated;
    [SerializeField] private UnityEvent onDeactivated;

    public void ActivateButton()
    {
        activeSprite.SetActive(true);
        deactiveSprite.SetActive(false);

        onActivated.Invoke();
    }

    public void DeactivateButton()
    {
        activeSprite.SetActive(false);
        deactiveSprite.SetActive(true);

        onDeactivated.Invoke();
    }
}
