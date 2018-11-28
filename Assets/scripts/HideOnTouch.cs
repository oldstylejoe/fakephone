using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnTouch : MonoBehaviour {

    [Header("A short fade delay for beauty.")]
    public float fadeTime = 0.2f;

    [Header("This is what fades.")]
    public CanvasGroup cv;

    private void OnMouseDown()
    {
        StopAllCoroutines();
        StartCoroutine(Fader());
    }

    /// <summary>
    /// Fade out the object (no destuction) after a touch event.
    /// </summary>
    private IEnumerator Fader()
    {
        for(float i = fadeTime; i > 0; i -= Time.deltaTime)
        {
            cv.alpha = i / fadeTime;
            yield return null;
        }
        gameObject.SetActive(false);
        yield return null;
    }
}
