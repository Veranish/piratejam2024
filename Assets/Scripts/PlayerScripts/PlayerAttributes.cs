using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour, IDamageable
{
    [Header("Light and Lamp properties")]
    public int LightLevel; //Functions as both light and HP.

    public Light lampLight; //Should be tied to the Light object in the player's lamp.

    [Header("Item and related attributes")]
    bool carryingItem;
    public GameObject itemCarried; // The resource that the player is carrying.
    public Transform carryPoint; //Where objects will be carried in physical space.

    [Header("Abilities")]
    bool canShoot; //if true, player is able to shoot.

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        UpdateLightAesthetic();
        carryingItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (carryingItem)
            {
                DropItem();
            }
            else
            {
                var ray = new Ray(this.transform.position, this.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10))
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {

                        interactable.Interact(this.gameObject);


                        //TODO: Make it check what it's interacting with.
                        //updateLightValue(LampLightCost);
                        //updateLightAesthetic();
                    }
                }
            }
        }
        if(Input.GetMouseButtonUp(1) && canShoot)
        {
            //Fire the lamp!


        }
    }

    public void Damage(int damageToDeal)
    {
        LightLevel -= damageToDeal;
        UpdateLightAesthetic();
        DeathCheck();
        //Debug.Log("Hurt! For: " + damageToDeal);
    }

    bool DeathCheck() //Returns true if light level is at death levels
    {
        if(LightLevel<= 0)
        { return true; }
        return false;
    }



    void UpdateLightAesthetic()
    {
        lampLight.intensity = (float)LightLevel / 18; //divided by 20 due to 20 being the max, so, normalized.
        if(LightLevel < 10) { lampLight.color = Color.red; }
        else if(LightLevel > 10) { lampLight.color = Color.yellow; }
    }

    //Called by a resource being interacted with. Returns true if successful, false if not.
    public bool TryPickUpItem(GameObject newItemGO)
    {
        Debug.Log("Attempting to carry");
        if (itemCarried == null)
        {
            Debug.Log("Carrying item");
            itemCarried = newItemGO;
            itemCarried.transform.parent = carryPoint;
            itemCarried.transform.position = carryPoint.position;
            itemCarried.GetComponent<Rigidbody>().detectCollisions = false;
            itemCarried.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            itemCarried.GetComponent<Rigidbody>().freezeRotation = true;
            itemCarried.GetComponent<Rigidbody>().useGravity = false;
            carryingItem = true;

            return true;
        }
        return false;
    }

    public void DropItem()
    {
        itemCarried.transform.parent = null;
        itemCarried.GetComponent<Rigidbody>().detectCollisions = true;
        itemCarried.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        itemCarried.GetComponent<Rigidbody>().freezeRotation = false;
        itemCarried.GetComponent<Rigidbody>().useGravity = true;
        carryingItem = false;
        itemCarried = null;
    }

}
