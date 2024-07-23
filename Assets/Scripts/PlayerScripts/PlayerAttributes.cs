using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour, IDamageable
{
    [Header("Light and Lamp properties")]
    public int LightLevel; //Functions as both light and HP.
    bool canShoot; //if true, player is able to shoot.
    public Light lampLight; //Should be tied to the Light object in the player's lamp.


    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        UpdateLightAesthetic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null) { 
                    interactable.Interact(this.gameObject);

                    //TODO: Make it check what it's interacting with.
                    //updateLightValue(LampLightCost);
                    //updateLightAesthetic();
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



}
