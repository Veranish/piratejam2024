using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// I'm hemmed myself in here with the implementation I have....

// I want ot free this up and basically have a controller script that calls thisthe FadeText function, but control
// iterating through the text boxes there, so I can break out easier on a click from the player to skip

public class IntroFadeIn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToUse;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float newAlpha;
    [SerializeField] private float delayBeforeStart;
    [SerializeField] private float timeBetweenFadeFrames;
    private bool userSkipped = false;
    // list of all text boxes to make sure and set all alphas to 1 (or something else visible) if we skip
    // private list
    private bool shouldWeSkip = false;
    private bool areWeDone = false;
    // [SerializeField] private float timeMultiplier = 1f;
    // [SerializeField] private float something; // forgot what I was doing here

    private void Start()
    {
        StartCoroutine(FadeText(textToUse, fadeDuration, newAlpha, delayBeforeStart, timeBetweenFadeFrames));
    }

    private void Update()
    {
        // look for mouse click and turn all delays to 0
        // no worky.  Detects click but does not get new values into the while loop...
        // maybe set a public variable flag here, and look for that flag in the while loop...
        if (Input.GetMouseButtonDown(0))
        {
            userSkipped = true;
            Debug.Log("User clicked");
        }
    }


    IEnumerator FadeText(TextMeshProUGUI textToUse, float fadeDuration, float newAlpha, float delayToBegin, float frameDelay)
    {
        if (userSkipped == true)
        {
            delayToBegin = 0f;
        }
        yield return new WaitForSeconds(delayToBegin);
        float originalAlpha = textToUse.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration) // add condition to look for mouse click
        {
            if (userSkipped == true)
            {
                fadeDuration = 0f;
                delayToBegin = 0f;
                frameDelay = 0f;
            }
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(originalAlpha, newAlpha, elapsed / fadeDuration);
            textToUse.color = new Color(textToUse.color.r, textToUse.color.g, textToUse.color.b, alpha);
            // yield return null;
            yield return new WaitForSeconds(frameDelay);
        }
    }

    private void SkipToTheEnd ()
    {
        // immediately fade all text all the way in and change scene to main game level 1
    }
}
