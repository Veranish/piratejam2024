using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour, IInteractable
{
    public Hopper leftHopper;
    public Hopper rightHopper;
    public Transform itemDropPoint;

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
        Debug.Log("Interact with workbench fired.");
        if (leftHopper != null && rightHopper != null)
        {
            CheckRecipe();
        }
        else
        {
            //TODO: Add "Both hoppers must be full!" message
        }
    }

    void CheckRecipe()
    {
        ResourceItem item1 = leftHopper.GetComponent<ResourceItem>();
        ResourceItem item2 = rightHopper.GetComponent<ResourceItem>();

        foreach (Recipe i in item1.possibleRecipes)
        {
             if (item2 == item1.possibleRecipes[1].itemTwo)
             {
                Instantiate(i.resultItem, itemDropPoint);
             }
        }
    }


}
