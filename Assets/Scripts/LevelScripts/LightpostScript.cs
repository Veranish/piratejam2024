using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightpostScript : MonoBehaviour, IInteractable
{
    bool isLit;
    public Light myLight;
    public float litIntensity = 1; //Initial 1, configurable in editor. Range is 0-8.
    // Start is called before the first frame update
    void Start()
    {
        if (isLit) { lightLamp(); }
        else { myLight.intensity = 0; }
    }

    public void Interact(GameObject interactor)
    {
        if (!isLit){lightLamp();}
    }

    void lightLamp()
    {
        isLit = true;
        myLight.intensity = litIntensity;

    }

}
