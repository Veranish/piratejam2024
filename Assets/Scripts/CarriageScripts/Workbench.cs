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
        Recipe[] LoadedRecipes = Resources.LoadAll<Recipe>("ScriptableObjects/");
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
        Debug.Log("Checking for a recipe");
        ResourceItem item1 = leftHopper.storedItem.GetComponent<ResourceItem>();
        ResourceItem item2 = rightHopper.storedItem.GetComponent<ResourceItem>();

        Debug.Log(item1.possibleRecipes[0].recipeName);

        foreach (Recipe i in item1.possibleRecipes)
        {
            Debug.Log("We're in foreach!");
            Debug.Log(item2.resName);
            Debug.Log(i.itemTwo.GetComponent<ResourceItem>().resName);
            if (item2.resName == i.itemTwo.GetComponent<ResourceItem>().resName)
             {
                Debug.Log("Recipe found, Instantiating");
                Instantiate(i.resultItem);
             }
        }
    }


}
