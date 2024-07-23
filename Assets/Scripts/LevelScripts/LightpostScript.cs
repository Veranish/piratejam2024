using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightpostScript : MonoBehaviour, IInteractable
{
    bool isLit;
    public Light myLight;
    public float litIntensity = 1; //Initial 1, configurable in editor. Range is 0-8.
    public int lightDamage = 15; //How much damage is done to the player to light the light. Configurable, 15 for now.
    
    // Start is called before the first frame update
    
    void Start()
    {
        if (isLit) 
        { 
            lightLamp(); 
        }
        else 
        { 
            myLight.intensity = 0;
        }
    }

    public void Interact(GameObject interactor)
    {
        if (!isLit)
        {
            lightLamp();
            IDamageable damageable = interactor.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(lightDamage);
            }
        }

    }

    void lightLamp()
    {
        isLit = true;
        myLight.intensity = litIntensity;
    }

}
