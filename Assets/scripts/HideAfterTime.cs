/* Joe Snider
 * 4/18
 * 
 * Fade out the object this is attached to after some time.
 * Requires a CanvasGroup on the parent.
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterTime : MonoBehaviour {

    [Header("Control the fade action")]
    public float fadeStartTime = 1.0f;
    public float fadeDurationTime = 1.0f;

    [Header("This is what fades.")]
    public CanvasGroup cv; 

    private void OnEnable()
    {
        StopAllCoroutines();
        cv.alpha = 1.0f;
        StartCoroutine(Fader());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Fader()
    {
        yield return new WaitForSeconds(fadeStartTime);

        for(float f = fadeDurationTime; f > 1.0e-6; f -= Time.deltaTime)
        {
            cv.alpha = f / fadeDurationTime;
            yield return null;
        }

        yield return null;
        gameObject.SetActive(false);
    }

}
