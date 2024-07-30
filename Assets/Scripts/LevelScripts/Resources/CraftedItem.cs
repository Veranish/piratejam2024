using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftedItem : MonoBehaviour, IInteractable
{

    public string itemName; //name of the item
    public string description; //Description of the item
    public GameObject prefab; //Prefab that will spawn if item is dropped.
    public bool interactedBefore = false; // flag to display hint or not
    private DialogueScript dialogueScript; // so we can display hints
    public int healthModifier; //How much health it modifies. This will need to change in the future for different item types.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject interactor) //Identical to ResourceItem.
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
                    // dialogueScript.StartDialogue();
                    interactedBefore = true;
                }
                
            }
        
    }
}
