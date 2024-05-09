using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingControl : MonoBehaviour
{
    public GameObject povObj;
    public GameObject viewModel;

    public GameObject wrist;

    public GameObject thumbTip;
    public GameObject indexTip;
    public GameObject middleTip;
    public GameObject ringTip;

    public float proximityThreshold;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(wrist.transform.localRotation.eulerAngles.z);
        
        float localWristZRot = wrist.transform.localRotation.eulerAngles.z;
        if (100f < localWristZRot && localWristZRot < 250f)
        {
            Vector3 wristPos = wrist.transform.position;    
            Vector3 indexTipPos = indexTip.transform.position;
            Vector3 middleTipPos = middleTip.transform.position;
            Vector3 ringTipPos = ringTip.transform.position;



            if (Vector3.Distance(wristPos, ringTipPos) > proximityThreshold)
            {
                Debug.Log("Cam 3");
                povObj.transform.localPosition = new Vector3(0f, 0.6f, -6.5f);
                // show plane
                viewModel.SetActive(true);
            }
            else if (Vector3.Distance(wristPos, middleTipPos) > proximityThreshold)
            {
                
                Debug.Log("Cam 2");
                povObj.transform.localPosition = new Vector3();
                // show nothing
                viewModel.SetActive(false);
            }
            else if (Vector3.Distance(wristPos, indexTipPos) > proximityThreshold)
            {
                Debug.Log("Cam 1");
                povObj.transform.localPosition = new Vector3();
                // show cockpit
                viewModel.SetActive(true);
            }
        }
    }
}
