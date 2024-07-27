using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    // declare text to display
    //public Text gameOverMessage;
    // declare list of strings for game over message choices
    public List<string> gameOverList = new List<string> {
        "Another falls, consumed by Shadow. Will the Sun ever rise?",
        "What shallow Faith.",
        "The Lantern, cracked. The Source, ravaged. Who will light the path?",
        "Flickering, dying, the last gasp of flame.",
        "Another will take your place.",
        "You lack tenacity.",
        "Fallen! Fallen! The Light dims!",
        "The sickness in this world prevails.",
        "No further, little ember?",
        "Where is your strenght?",
        "A step closer to Perpetual Night."
    };
    public TMP_Text gameOverText;

    // Create a Random object
    public System.Random myChoice = new System.Random();


    void Start()
    {
        gameOverText.text = ChooseMessage();
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
}
