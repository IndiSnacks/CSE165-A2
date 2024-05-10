using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject checkPointManager;

    private void Start() {
        checkPointManager = GameObject.FindGameObjectsWithTag("CheckPointManager")[0];
        Debug.Log(checkPointManager);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            checkPointManager.GetComponent<RaceTrackParse>().CheckPointObserver(this.gameObject);
        }
    
    }
}
