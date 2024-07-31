using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class CarriageAnimateSpline : MonoBehaviour, IInteractable, IDamageable
{
    bool isMoving;
    public Light carriageLight;
    public SplineAnimate mySplineA;
    public SphereCollider playerTooFarAwayCollider;

    public int lightLevel;
    public int lightMax = 100;
    public GameState myGameState;

    void Start()
    {
        isMoving = false;
    }

    void Update()
    {
        if(mySplineA.NormalizedTime > 0.95)
        {
            Debug.Log("Carriage made it to the end!");
            myGameState.CarriageArrived();
        }
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

    public void Damage(int damageToTake)//Also use this to heal haha, just make the value negative.
    {
        lightLevel -= damageToTake;
        UpdateLightAesthetic();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerAttributes>() is not null)
        {
           // StopMove();
           //TODO: This triggers whenever any player collider leaves any carriage collider, will need to refactor.
        }
    }

    void UpdateLightAesthetic()
    {
        //TODO: Make a light and all this work as copied from playerattributes
        carriageLight.intensity = (float)lightLevel / lightMax; //divided by 20 due to 20 being the max, so, normalized.
        if (lightLevel <= 30) { carriageLight.color = Color.red; }
        else if (lightLevel > 30 && lightLevel < 50) { carriageLight.color = Color.yellow; }
        else { carriageLight.color = Color.white; }
    }
}
