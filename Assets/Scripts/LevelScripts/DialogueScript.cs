using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    // public string[] lines;
    public string textToDisplay;
    public float textSpeed;
    private int index;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (textToDisplay != null)
            {
                if (textComponent.text == textToDisplay)
                {
                    CloseIt();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = textToDisplay;
                }
            }
        }
    }

    public void ShowHint(string textToShow)
    {
        textToDisplay = textToShow;
        StartCoroutine(TypeLine(textToShow));
    }

    IEnumerator TypeLine(string hint)
    {
        foreach (char c in hint.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // not sure I need this.  Basically just want to keep showing the text, clicking to skip to end and clicking to make it go away
    void CloseIt()
    {
        // no, need to keep it
        // gameObject.SetActive(false);
        textComponent.text = "";
        textToDisplay = "";
    }

}
