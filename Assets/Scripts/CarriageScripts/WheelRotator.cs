using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{

#region Variables

    [Header("Wheel Rotation Config")]
    

    public float wheelRadius = 2f;
    const float TWOPI = Mathf.PI * 2f;
    public Vector3 forwardAxis = Vector3.forward;
    public Vector3 rightAxis = Vector3.up;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    public List<Transform> wheelTransforms = new List<Transform>();
    struct WheelData
    {
        public Transform wheelTransform;
        public float axleRadius;
        public float wheelRadius;
        public Vector3 rotationAxis; //local axis in mesh hierarchy
        public int rotationSign; //"left" or "right" side of vehicle for turning

        public WheelData(Transform wheelTransform, Transform vehiclePivot, float wheelRadius, Vector3 vehicleForward, Vector3 vehicleRight)
        {
            this.wheelTransform = wheelTransform;
            Vector3 vecToWheel = wheelTransform.localPosition; 
            this.wheelRadius = wheelRadius;
            this.axleRadius = Vector3.ProjectOnPlane(vecToWheel, vehiclePivot.up).magnitude; //distance from the vehicle pivot for turning calculations

            this.rotationAxis = wheelTransform.InverseTransformDirection( //the "axel" of the wheel transform to rotate around
                vehiclePivot.TransformDirection(vehicleRight)
            );
            
            this.rotationSign = Vector3.Dot(vehicleRight, vecToWheel) > 0f ? -1 : 1;
        }
    };

    // wheels to be referenced for rotation
    private List<WheelData> wheels = new List<WheelData>();

    #endregion Variables

    #region Init / Shutdown

    // Start is called before the first frame update
    void Awake()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;  

        foreach (Transform t in wheelTransforms)
        {
            wheels.Add(new WheelData(t, transform, wheelRadius, forwardAxis, rightAxis));
        }
    }

    #endregion Init / Shutdown

    // Update is called once per frame
    void Update()
    {
        AnimateWheels();
    }

    void AnimateWheels()
    {
        Vector3 velocity = (transform.position - lastPosition);
        float forwardVelocity = Vector3.Dot(transform.TransformDirection(forwardAxis), velocity);


        Vector3 curForward = transform.rotation * forwardAxis;
        Vector3 prevForward = lastRotation * forwardAxis;

        Vector3 curRight = transform.rotation * rightAxis;
        Vector3 vehicleObjectUp = Vector3.Cross(curRight, curForward);


        float angularVelocity = Vector3.SignedAngle(prevForward, curForward, vehicleObjectUp);

        
        //set prev data////////////////////////////
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        

        foreach (WheelData d in wheels)
        {
            float wheelRotationAngleLinear = calcWheelRotationForForward(d.wheelRadius, forwardVelocity); //just for moving forward
            float wheelRotationAngleAngular = calcWheelRotationForTurn(d.axleRadius, angularVelocity) * d.rotationSign;

            Vector3 alignedRotation = d.rotationAxis * (wheelRotationAngleLinear - wheelRotationAngleAngular);
            d.wheelTransform.Rotate(alignedRotation);
        }
    }

    private float calcWheelRotationForForward(float radius, float distance)
    {
        return (distance / Mathf.PI * radius) * 360f;
    }


    private float calcWheelRotationForTurn(float radius, float angle)
    {
        return Mathf.PI * radius * angle;
    }
}
