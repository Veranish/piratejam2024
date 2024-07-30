using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : MonoBehaviour, IInteractable
{
    public string resName; //name of the resource
    public string description; //Description of the resource
    public GameObject prefab; //Prefab that will spawn if item is dropped.
    public bool interactedBefore = false; // flag to display hint or not
    private DialogueScript dialogueScript; // so we can display hints

    public Recipe[] possibleRecipes; //List of potential recipes. Will be configured in-editor in prefabs.


    // Start is called before the first frame update
    void Start()
    {
        // grab the dialouge script so we can show hints
        dialogueScript = GameObject.Find("PopupHintPanel").GetComponent<DialogueScript>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact(GameObject interactor)
    //     public void Interact(GameObject interactor, bool hintCheck)
    {
        Debug.Log("Item interact fired");
        PlayerAttributes playerRef = interactor.GetComponent<PlayerAttributes>();

        if (playerRef != null)
        {
            Debug.Log("Player ref not null");
            // may need to deconflict display text here, in case it bottlenecks other interact functions
            if (interactedBefore == false)
            {
                // call display dialogue function passing the description
                dialogueScript.ShowHint(description);
                //dialogueScript.StartDialogue();
                interactedBefore = true;
            }
            if (playerRef.TryPickUpItem(this.gameObject))
            {


                //Warning: This may or may not delete the ref it passes? Gotta check.
            }
        }
    }

    public virtual void Drop(Transform locToDrop)
    {
        Instantiate(prefab, locToDrop);
    }


}
