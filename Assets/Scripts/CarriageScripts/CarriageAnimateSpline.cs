using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class CarriageAnimateSpline : MonoBehaviour, IInteractable, IDamageable
{
    bool isMoving;

    public SplineAnimate mySplineA;
    public SphereCollider playerTooFarAwayCollider;

    public int lightLevel;
    public int lightMax = 100;

    void Start()
    {
        isMoving = false;
    }

    void Update()
    {

    }

    void StartMove()
    {
        isMoving = true;
        mySplineA.Play();
    }

    void StopMove()
    {
        isMoving = false;
        mySplineA.Pause();
    }

    public void Interact(GameObject interactor)
    {
        if (!isMoving)
        {
            StartMove();
            isMoving = true;
        }
        else
        {
            StopMove();
            isMoving = false;
        }
    }

    public void Damage(int damageToTake)
    {
        lightLevel -= damageToTake;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerAttributes>() is not null)
        {
            StopMove();
        }
    }

    void UpdateLightAesthetic()
    {
        //TODO: Make a light and all this work as copied from playerattributes
       // lampLight.intensity = (float)LightLevel / 18; //divided by 20 due to 20 being the max, so, normalized.
        //if (LightLevel < 10) { lampLight.color = Color.red; }
       // else if (LightLevel > 10) { lampLight.color = Color.yellow; }
    }
}
