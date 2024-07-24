using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour, IInteractable
{
    // declare text to display
    public Text gameOverMessage;
    // declare list of strings for game over message choices
    public List<string> gameOverList = new List<string> {
        "Another falls, consumed by Shadow. Will the Sun ever rise?",
        "What shallow Faith.",
        "The Lantern, cracked. The Source, ravaged. Who will light the path?",
        "Flickering, dying, the last gasp of flame.",
        "Another will take your place.",
        "You lack tenacity.",
        "Fallen! Fallen! The Light dims!"
    };
    // declare image/screen we're going to use to fade out
    public Image blackScreen;
    // define how long it takes to fade the screen
    public float fadeToBlackDuration = 3;
    // Create a Random object
    public System.Random myChoice = new System.Random();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // check here carriage light level
        // don't need this check every frame
        // every .5 second should do, so figure that out
    }

    public void Interact(GameObject interactor)
    {
            // FadeToBlack();
        //    SceneChanger.Instance.ChangeScene("JDG_Dev Scene");
    }

    // function/method to choose the message from the list
    // consider passing the list in as a parameter to extend functionality
    public string ChooseMessage()
    {
        // rando choose one by doing some silly c# stuff

        // Get a random index
        int randomIndex = myChoice.Next(gameOverList.Count);
        // Retrieve the random element
        string actualMessage = gameOverList[randomIndex];

        // return choice
        return actualMessage;
    }

    // function/method to display the game over messaage
    // currently throws compile erros so commenting out for now
    /*
    void DisplayMessage()
    {
        // get the message by calling function/method ChooseMessage    *** CURRENTLY DOES NOT WORK ***
        public string theMessage = ChooseMessage();
        // draw the message
        // still need a valid brush and format (no idea yet what these are)
        // void DrawString(theMessage, System.Drawing.Font JazzCreateBubble, System.Drawing.Brush brush, float 200, float 200, System.Drawing.StringFormat format);
    }


    // function/method to fade to black
    public void FadeToBlack()
    {
        // define color of the new image*(screen)
        blackScreen.color = Color.black;
        // no idea what cavans render does, but assume it's setting the alpha here of the color we're setting to 0 so, transparent
        blackScreen.canvasRenderer.SetAlpha(0.0f);
        // here it's likely adding to the alpha a bit at a time, during the duration we declared, until it reaches 1 (so alpha is from 0 to 1 here, 0 being transparent, 1 being fully rendered, or not see through at all)
        blackScreen.CrossFadeAlpha(1.0f, fadeToBlackDuration, false);
    }
    */
}







// how are we tracking the light level
// carriage light level I imagine, which should dictate overall light on the level
// maybe milestone 2+ also dictates how far the light goes out from the carriage
// i.e. how close the baddies can encroach

/*
// when carriage light level reaches 0
if (carraigeLight == 0) then
{
    // fade out the scene
    // fade out any remaining scene audio
        // player
        // carraige
        // baddies
        // music
        // anything ambient
    // remove all player control


    // choose 'game over' text
    // fade in 'game over' text
    // fade in any 'game over' music/audio

    // fade out 'game over' text
    // fade out any remaining 'game over' music/audio

    // return player to main menu (probably a function call)
}
*/
