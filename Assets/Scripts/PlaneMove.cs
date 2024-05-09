using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

public class PlaneMove : MonoBehaviour
{
    public GameObject thrustObject;
    public GameObject thrustGripObject;
    public float maxSpeed;
    public float thrustSensitivity;

    public GameObject wrist;

    public GameObject directionObject;
    public float turnSensitivity;


    private bool isGripping = false;
    private bool justGripped = false;

    private float speedFactor;
    private float initialGripZ;
    private float currentGripZ;

    private Rigidbody rb;
    private Vector3 heading;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float localWristZRot = wrist.transform.localRotation.eulerAngles.z;
        if (!(100f < localWristZRot && localWristZRot < 250f))
        {
            Vector3 eulerRotation = directionObject.transform.rotation.eulerAngles;
            eulerRotation.z = 0;
            Quaternion follow = Quaternion.Euler(eulerRotation);

            float turnAcceleration = Quaternion.Angle(rb.transform.rotation, directionObject.transform.rotation) / 45f;

            // rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, directionObject.transform.rotation, turnSensitivity);
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, directionObject.transform.rotation, turnSensitivity * turnAcceleration);
        }
        
        

        Vector3 localGripRot = thrustGripObject.transform.localEulerAngles;
        // Debug.Log(localGripRot);

        isGripping = (45 < localGripRot.x && localGripRot.x < 120);

        //Debug.Log("grip angle: " + localGripRot.x);
        //Debug.Log("is gripping: " + isGripping);

        if (isGripping)
        {
            if (!justGripped)
            {
                justGripped = true;
                initialGripZ = rb.transform.InverseTransformPoint(thrustObject.transform.position).z;
            }
            currentGripZ = rb.transform.InverseTransformPoint(thrustObject.transform.position).z;
            speedFactor = Mathf.Clamp((currentGripZ - initialGripZ) * thrustSensitivity, 0, 1);
        }
        else
        {
            justGripped = false;
        }

        
        //Debug.Log("Speed factor: " + speedFactor);


        heading = rb.transform.forward;
        heading.Normalize();
        rb.velocity = heading * maxSpeed * speedFactor;

        /* =============== Direction ATTEMPT 1 =============== 
         * 
         * Matrix4x4 rbRotation = Matrix4x4.TRS(Vector3.zero, rb.transform.rotation, Vector3.one);
        Vector3 pitchHandRotation = pitchObject.transform.rotation.eulerAngles;
        localRotation = rbRotation.inverse * new Vector4(pitchHandRotation.x, pitchHandRotation.y, pitchHandRotation.z, 0);
        localXRot = localRotation.x;

        Debug.Log("localXRot: " + localXRot);

        if (0 < localXRot && localXRot <= 180)
        {
            pitchFactor = -localXRot / 90;
        } 
        else // localXRot (180, 360)
        {
            pitchFactor = (360 - localXRot) / 90;
        }

        Debug.Log("pitchFactor: " + pitchFactor);

        rb.transform.Rotate(-pitchFactor * 0.3f, 0, 0);*/

        /* =============== Direction ATTEMPT 2 =============== 
         * 
         * Quaternion localRotation = pitchObject.transform.rotation * Quaternion.Inverse(rb.transform.rotation);

        Vector3 localEuler = localRotation.eulerAngles;

        float localXRot = localEuler.x;
        float localYRot = localEuler.y;
        float localZRot = localEuler.z;

        float pitchFactor;
        float yawFactor;
        float rollFactor;

        Debug.Log("localZRot: " + localZRot);

        if (0 < localXRot && localXRot <= 180)
        {
            pitchFactor = localXRot / 90;
        }
        else // localXRot (180, 360)
        {
            pitchFactor = - (360 - localXRot) / 90;
        }

        if (0 < localYRot && localYRot <= 180)
        {
            yawFactor = localYRot / 90;
        }
        else // localYRot (180, 360)
        {
            yawFactor = -(360 - localYRot) / 90;
        }

        if (0 < localZRot && localZRot <= 180)
        {
            rollFactor = localZRot / 90;
        }
        else // localZRot (180, 360)
        {
            rollFactor = -(360 - localZRot) / 90;
        }


        rb.transform.Rotate(pitchFactor, 0, 0);
        rb.transform.Rotate(0, yawFactor, 0);
        rb.transform.Rotate(0, 0, rollFactor);*/
    }
}
