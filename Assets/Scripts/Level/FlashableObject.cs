using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashableObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer flashObject;
    [SerializeField] private float visibleTime;
    [SerializeField] private float fadeOutTime;

    public void PlayerFlashed() {
        StartCoroutine(TempFlashObject());
    }

    IEnumerator TempFlashObject() {
        yield return new WaitForSeconds(0.3f);

        flashObject.color = new Color(1f, 0.95294f, 0.86274f, 0.3f);

        yield return new WaitForSeconds(visibleTime);

        float elapsedTime = 0;
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0.3f, 0, elapsedTime / fadeOutTime);
            flashObject.color = new Color(flashObject.color.r, flashObject.color.g, flashObject.color.b, newAlpha);
            yield return null;
        }
    }
}
