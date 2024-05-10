using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wayfinder : MonoBehaviour
{
    [SerializeField] private Component lineRenderer;
    [SerializeField] private GameObject checkPointManager;

    private Vector3 endPt;

    private void Start() {
        checkPointManager = GameObject.FindGameObjectsWithTag("CheckPointManager")[0];
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void FixedUpdate() {
        endPt = checkPointManager.GetComponent<RaceTrackParse>().nextCheckpoint.transform.position;
        ((LineRenderer)lineRenderer).SetPosition(0, transform.position);
        ((LineRenderer)lineRenderer).SetPosition(1, endPt);
    }
}
