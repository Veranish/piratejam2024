using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private void Start()
    {
        // make sure we give the cursor back
        Cursor.lockState = CursorLockMode.None;
        // but lock it to the screen (do we want this??)
        Cursor.lockState = CursorLockMode.Confined;

    }


    /*
    public void Play()
    {
        // if player hits the play button, swap to the next scene (iterate from 0 - menu to 1 - first game level)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */

    public void Quit()
    {
        // if player hits the quit button, exit the game
        // maybe steal fade out, and fade out done from scene transitions to exit elegantly
        Application.Quit();
    }
}
