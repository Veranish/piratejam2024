using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : MonoBehaviour, IInteractable
{
    public string resName; //name of the resource
    public string description; //Description of the resource
    public GameObject prefab; //Prefab that will spawn if item is dropped.


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("Item interact fired");
        PlayerAttributes playerRef = interactor.GetComponent<PlayerAttributes>();
        
        if (playerRef != null)
        {
            Debug.Log("Player ref not null");
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
