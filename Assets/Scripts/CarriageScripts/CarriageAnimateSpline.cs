using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class CarriageAnimateSpline : MonoBehaviour, IInteractable
{
    bool isMoving;
    public SplineAnimate mySplineA;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;

        //GetComponent<SplineAnimate>().Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startMove()
    {
        isMoving = true;
        mySplineA.Play();
    }
    void stopMove()
    {
        isMoving = false;
        //if(GetComponent<SplineAnimate>().isPlaying())
        {
            mySplineA.Pause();
        }
    }

    public void Interact(GameObject interactor)
    {
        if (!isMoving)
        {
            startMove();
        }
        else { stopMove(); }
    }
}
