using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircleWipe : SceneTransition
{
    public Image circle;
    public float transitionDuration = 1f; // Duration for the transition

    public override IEnumerator AnimateTransitionIn()
    {
        // Move the circle off-screen to the left
        circle.rectTransform.anchoredPosition = new Vector2(-1000f, 0f);
        // Animate it to the center
        yield return MoveToPosition(0f);
    }

    public override IEnumerator AnimateTransitionOut()
    {
        // Animate the circle to move off-screen to the right
        yield return MoveToPosition(1000f);
    }

    private IEnumerator MoveToPosition(float targetX)
    {
        float startX = circle.rectTransform.anchoredPosition.x;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / transitionDuration);
            circle.rectTransform.anchoredPosition = new Vector2(newX, circle.rectTransform.anchoredPosition.y);
            yield return null;
        }

        // Ensure the final position is set precisely
        circle.rectTransform.anchoredPosition = new Vector2(targetX, circle.rectTransform.anchoredPosition.y);
    }
}
