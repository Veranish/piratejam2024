using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//This script began from one taken from user NairelPrandini, in the Unity Forums. Thanks!
//modified by Veranish (Added look rotation behavior)
public class PlayerMove : MonoBehaviour
{


    CharacterController Controller;

    public float Speed;
    public Transform cam;


    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        Vector3 Movement = cam.transform.right * Horizontal + cam.transform.forward * Vertical;
        Movement.y = 0f;
        Controller.Move(Movement);

        transform.rotation = Quaternion.LookRotation(Movement);

    }

}