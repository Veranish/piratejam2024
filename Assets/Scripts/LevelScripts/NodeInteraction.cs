// script for handling resource node on player interaction
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeInteractionScript : MonoBehaviour, IInteractable
{
    // declare and define gathering bool
    bool isGathered = false;
    public float nodeGatherDelay = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if player interact with object type, or with any object in an array of objects (but that means checking the whole array each frame, seems no good)
        // call function to remve the object by passing in the object the player interacted with
    }

    public void Interact(GameObject interactor)
    {
        if (!isGathered)
        {
            GatherNode();
        }
    }

    void GatherNode()
    {
        isGathered = true;

        // yield return new WaitForSeconds(3);
        // delete object interacted with after 2 second nodeGatherDelay
        Destroy (gameObject,2);


        // fire/call carrying and/or player resource addition

        // on player interact (for now skip to remove object)
          // later milestone
          // play audio
          // play animation
            // depending on how we implement
            // wait a set time/for audio to complete/for animation to complete
            // maybe stop audio/animation
    }
}
