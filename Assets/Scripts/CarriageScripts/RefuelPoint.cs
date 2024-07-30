using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelPoint : MonoBehaviour
{
    public GameObject carriageRef; //set in editor, or get via parent.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<CraftedItem>())
        {
            carriageRef.GetComponent<CarriageAnimateSpline>().Damage(collision.gameObject.GetComponent<CraftedItem>().healthModifier);
        }
    }
}
