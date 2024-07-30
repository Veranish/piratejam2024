using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Recipe", order = 1)]
public class Recipe: ScriptableObject
{

    public string recipeName;
    public GameObject itemOne; //Resource Item prefab
    public GameObject itemTwo; //Resource item prefab
    public GameObject resultItem; //Crafted item prefab
}
