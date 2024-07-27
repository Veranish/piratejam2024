using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroFadeIn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToUse;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float newAlpha;
    [SerializeField] private float delayBeforeStart;
    // list of all text boxes to make sure and set all alphas to 1 (or something else visible) if we skip
    // private list
    private bool shouldWeSkip = false;
    private bool areWeDone = false;
    // [SerializeField] private float timeMultiplier = 1f;
    // [SerializeField] private float something; // forgot what I was doing here

    private void Start()
    {
        StartCoroutine(FadeText(textToUse, fadeDuration, newAlpha, delayBeforeStart));
    }

    private void Update()
    {
        /*
        if (FadeIncomplete)
        {
            StartCoroutine(FadeOutText(timeMultiplier, textToUse));
        }
        */
        // look for mouse click to skip to the end
    }


    IEnumerator FadeText(TextMeshProUGUI textToUse, float fadeDuration, float newAlpha, float delayToBegin)
    {
        yield return new WaitForSeconds(delayToBegin);
        float originalAlpha = textToUse.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration) // add condition to look for mouse click
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(originalAlpha, newAlpha, elapsed / fadeDuration);
            textToUse.color = new Color(textToUse.color.r, textToUse.color.g, textToUse.color.b, alpha);
            // yield return null;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SkipToTheEnd ()
    {
        // immediately fade all text all the way in and change scene to main game level 1
    }
}
