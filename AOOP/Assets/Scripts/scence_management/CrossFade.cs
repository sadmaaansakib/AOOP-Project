using System.Collections;
using UnityEngine;

[System.Serializable]
public class CrossFade : SceneTransition
{
    public CanvasGroup crossFade;
    public float fadeDuration = 1f; // Duration for the fade

    public override IEnumerator AnimateTransitionIn()
    {
        yield return Fade(1f);
    }

    public override IEnumerator AnimateTransitionOut()
    {
        yield return Fade(0f);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = crossFade.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            crossFade.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        // Ensure the final alpha value is set precisely
        crossFade.alpha = targetAlpha;
    }
}
