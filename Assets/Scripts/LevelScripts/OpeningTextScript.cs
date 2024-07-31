using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroFadeIn : MonoBehaviour
{
    public Animator animator;
    private TMP_Text textToUse;
    private float howLongToFade = 1f;
    private float newAlpha = 0.5f;
    private float timeBetweenFadeFrames = 0.1f;
    private float delayBeforeStart = 3f;
    private bool userSkippedBefore = false;

    // array of opening text text boxes
    private GameObject[] openingTextBoxes;
    private TMP_Text textComponent;
    private int textBoxIndex = 0;
    private bool userSkipped = false;

    private void Start()
    {
        // initialize and populate our array of text boxes
        openingTextBoxes = new GameObject[7];
        openingTextBoxes[0] = GameObject.Find("GameOpeningText_1");
        openingTextBoxes[1] = GameObject.Find("GameOpeningText_2");
        openingTextBoxes[2] = GameObject.Find("GameOpeningText_3");
        openingTextBoxes[3] = GameObject.Find("GameOpeningText_4");
        openingTextBoxes[4] = GameObject.Find("GameOpeningText_5");
        openingTextBoxes[5] = GameObject.Find("GameOpeningText_6");
        openingTextBoxes[6] = GameObject.Find("GameOpeningText_7");
        StartCoroutine(FadeText(delayBeforeStart, openingTextBoxes, howLongToFade, timeBetweenFadeFrames));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (userSkippedBefore == false)
            {
                userSkipped = true;
                // only let em skip once
                userSkippedBefore = true;
            }
        }
    }

    IEnumerator FadeText(float delayToBegin, GameObject[] openingTextBoxObjects, float fadeDuration, float fadeDelay)
    {
        // wait for it
        yield return new WaitForSeconds(delayToBegin);

        foreach (GameObject textObjects in openingTextBoxes)
        {
            // grab textMeshPro box
            textToUse = textObjects.GetComponent<TMP_Text>();

            // store original alpha
            float originalAlpha = textToUse.color.a;
            // start elapsed timer at 0 for each text box
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                if (userSkipped == true)
                {
                    break;
                }
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(originalAlpha, newAlpha, elapsed / fadeDuration);
                textToUse.color = new Color(textToUse.color.r, textToUse.color.g, textToUse.color.b, alpha);
                // yield return null;
                yield return new WaitForSeconds(fadeDelay);
            }
            textBoxIndex ++;
        }

        // make sure all text alphas are correct before we leave, in case we broke out of the loop
        foreach (GameObject textObjects in openingTextBoxes)
        {
            // grab textMeshPro box
            textToUse = textObjects.GetComponent<TMP_Text>();
            float originalAlpha = textToUse.color.a;
            if (originalAlpha != newAlpha)
            {
                textToUse.color = new Color(textToUse.color.r, textToUse.color.g, textToUse.color.b, newAlpha);
            }
        }
        // when we're done, transition to main game scene
        yield return new WaitForSeconds(1); // slight delay
        animator.SetTrigger("FadeOutTrigger");
    }
}
