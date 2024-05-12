using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCollisionManager : MonoBehaviour
{
    [SerializeField] private GameObject checkPointManager;
    [SerializeField] private GameManager gameManager;

    private void Start() {
        checkPointManager = GameObject.FindGameObjectsWithTag("CheckPointManager")[0];
        gameManager = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>();
        Debug.Log(checkPointManager);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            checkPointManager.GetComponent<RaceTrackParse>().CheckPointObserver(this.gameObject);
            gameManager.CheckPointReached();
        }
    
    }
}
