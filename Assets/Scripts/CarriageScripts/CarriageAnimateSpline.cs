using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class CarriageAnimateSpline : MonoBehaviour, IInteractable
{
    bool isMoving;

    public SplineAnimate mySplineA;

    public SphereCollider playerTooFarAwayCollider;

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
        }
        else
        {
            StopMove();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerAttributes>() is not null)
        {
            StopMove();
        }
    }
}
