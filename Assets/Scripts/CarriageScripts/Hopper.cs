using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopper : MonoBehaviour
{
    public BoxCollider hopperCollider;
    public GameObject storedItem;
    public Transform carryPoint;
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
        Debug.Log("Hopper collision triggered");
        if(storedItem == null)
        {
            Debug.Log("Stored item is null");
            if (collision.gameObject.GetComponent<ResourceItem>())
            {
                storedItem = collision.gameObject;
                //Todo: Visually represent and lock the resource
                Debug.Log("Hopper Carrying item");

                storedItem.transform.parent = carryPoint;
                storedItem.transform.position = carryPoint.position;
                storedItem.GetComponent<Rigidbody>().detectCollisions = false;
                storedItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                storedItem.GetComponent<Rigidbody>().freezeRotation = true;
                storedItem.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
}
