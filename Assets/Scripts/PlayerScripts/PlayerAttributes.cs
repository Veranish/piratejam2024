using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Light and Lamp properties")]
    public int LightLevel; //Functions as both light and HP.
    public int LampLightCost; //How much lighting a lamp will cost in lamp/hp value
    bool canShoot; //if true, player is able to shoot.
    public Light lampLight; //Should be tied to the Light object in the player's lamp.


    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        updateLightAesthetic();
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
                    updateLightValue(LampLightCost);
                    updateLightAesthetic();
                }
            }
        }
        if(Input.GetMouseButtonUp(1) && canShoot)
        {
            //Fire the lamp!
            

        }
    }

    public bool updateLightValue(int deltaLight)//If it's a hurt, this should be negative. Returns true if dead.
    {
        LightLevel += deltaLight;
        if(LightLevel < 0 ) { return true; }//Returns true if player is under 0, and thus, dead.


        return false;
    }

    void updateLightAesthetic()
    {
        lampLight.intensity = (float)LightLevel / 18; //divided by 20 due to 20 being the max, so, normalized.
        if(LightLevel < 10) { lampLight.color = Color.red; }
        else if(LightLevel > 10) { lampLight.color = Color.yellow; }
    }



}
