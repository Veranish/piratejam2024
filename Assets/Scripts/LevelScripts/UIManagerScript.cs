using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    // define hint strings, etc. here
    public string cedarHint = "The distinct scent of cedar has pulled you, beckoning, the promise of Light. You must scavenge from the ground like an animal. Oh for an axe!";
    public string carraigeHint = "Fortress and Purpose! A manufacture of resource and a haven, both. With this you will carry the Source to those bereft. Protect it!";
    public string lampPostHint  = "";
    public string roadHint = "The path is worn and twisting, a dilapidated vestige of its glory days. The lampposts that flank the road are empty, night-stolen. This way was safe once.";
    public string shadowHint = "Light hater! Night-spawn! Drive it away, subsume it with Holy Light!W";


    // may need int index for each pop up hint as a way to track if this is the first time or not

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    // create a function to call from playerattributes that will check what it's interacting with, check if we've interacted with that type before, and display the pop up if we have not

    public void PopUpHint()
    {
        Debug.Log("To Hint or not to Hint");
        /*
        PlayerAttributes playerRef = interactor.GetComponent<PlayerAttributes>();

        if (playerRef != null)
        {
            Debug.Log("Player ref not null");
            if (playerRef.TryPickUpItem(this.gameObject))
            {


                //Warning: This may or may not delete the ref it passes? Gotta check.
            }
        }
        */
    }
}





// when player interacts with an object, get it's type(?)
// check if this is the first interaction by referencing a public int index
   // all indexes tracked for each type
   // defined to 0 at game Start
   // and incremented once player has seen it to avoid seeing it again by simple if check
// if it's the first time, fade in the appropriate hint, display for a few seconds, fade out, then destroy
