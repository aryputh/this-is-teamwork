using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModalWindowManager : MonoBehaviour
{
    [SerializeField] private Animator modalAnim;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private GameObject warningObject;

    public void OpenWindow(bool openWin) {
        if (openWin) {
            modalAnim.SetInteger("state", 2);
        } else {
            modalAnim.SetInteger("state", 1);
        }
    }

    public void SetTitle(string newTitle) {
        titleText.text = newTitle;
    }

    public void SetDescription(string newDesc) {
        descText.text = newDesc;
    }

    public void SetIsWarning(bool isWarning) {
        warningObject.SetActive(isWarning);
    }
}
