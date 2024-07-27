using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text textMeshPro; // Reference to the TextMeshPro component
    public float typingSpeed = 0.05f; // Speed of typing in seconds
    public bool typewriterDone = false;

    private string fullText; // The complete text to be typed

    private void Start()
    {
        fullText = textMeshPro.text; // Store the full text
        textMeshPro.text = string.Empty; // Clear the text
        StartCoroutine(TypeText()); // Start typing animation
    }

    // Update is called once per frame
    void Update()
    {
        // look for mouse click
        if (Input.GetMouseButtonDown(0))
        {
          typingSpeed = SkipText();
        }
    }

    // Coroutine to simulate typing effect
    IEnumerator TypeText()
    {
        typewriterDone = false;  // we're typing here
        // dealy before we start
        yield return new WaitForSeconds(1);
        foreach (char letter in fullText)
        {
            textMeshPro.text += letter; // Append each letter to the text
            yield return new WaitForSeconds(typingSpeed); // Wait for the specified duration
        }
        // done typing
        typewriterDone = true;  // we're typing here
    }

    // Coroutine to simulate typing effect
    public float SkipText()
    {
        float skipTypingSpeed = 0f;
        return skipTypingSpeed;
    }

    // need an update function to look for mouse click to skip
    //basically look for mouse click, then call function to speed type speed to 0 (no delay)
}
