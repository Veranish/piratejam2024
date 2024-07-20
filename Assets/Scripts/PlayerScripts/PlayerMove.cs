using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script began from one taken from user NairelPrandini, in the Unity Forums. Thanks!
//modified by Veranish (Added look rotation behavior)
public class PlayerMove : MonoBehaviour
{


    CharacterController Controller;
    public float Speed; //Speed of character moving.
    public Transform cam; //Used to determine left, right, and forward vectors for input.
    public float crossPBuffer = 0.2f; //Used to determine when movement vector changes from forward to up. 


    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        float Horizontal = Input.GetAxis("Horizontal")  * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical")  * Speed *  Time.deltaTime;
        Vector3 down = new Vector3(0, -1, 0);
        Vector3 crossP = Vector3.Cross(cam.transform.forward, down);
        Vector3 VertVec = cam.transform.forward;
        //Debug.Log("CrossP sqr Magnitude:" + crossP.sqrMagnitude);
        if (crossP.sqrMagnitude < crossPBuffer) // This value is kinda wonky, could us more testing to find the right balance.
        {
            VertVec = cam.transform.up;
            
        }
        //whaaaat if we normalized the vector instead to 1 hmmmmmm
        // Nah normalizing vectors causes weird behavior
        Vector3 Movement = cam.transform.right * Horizontal + VertVec * Vertical;
        Movement.y = 0f;
        //Movement = Movement.normalized * Speed;
        if (Movement.sqrMagnitude > 0.0001f)
        {
            
            Controller.Move(Movement);
            transform.rotation = Quaternion.LookRotation(Movement);
        }
        
        if(Input.GetMouseButtonDown(0)) 
        {
            var ray = new Ray(this.transform.position, this.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 10))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null) { interactable.Interact(this.gameObject); }
            }
        }

    }

}