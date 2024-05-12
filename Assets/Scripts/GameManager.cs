using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool Started = false;
    public int checkPointsLeft;
    public float timelimit = 3.0f;
    private bool startCountDown = false;
    private bool startStopwatch = false;
    private float startTime = 0.0f;
    public int currCheckPoint;

    [SerializeField] private GameObject CheckpointManager;
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody PlayerRb;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Text;
    [SerializeField] private GameObject Stopwatch;
    [SerializeField] private Component Countdown;
    // Start is called before the first frame update
    void Start()
    {
        CheckpointManager = GameObject.FindGameObjectsWithTag("CheckPointManager")[0];
        PlayerRb = Player.GetComponent<Rigidbody>();
        checkPointsLeft = CheckpointManager.GetComponent<RaceTrackParse>().checkpoints.Count;

        Text.GetComponent<TextMeshProUGUI>().SetText("Thumb Up to Start");

        Player.GetComponent<PlaneMove>().enabled = false;
    }

    public void StartGame()
    {
        if(!Started){
            Text.GetComponent<TextMeshProUGUI>().SetText(timelimit.ToString("F0"));
            startCountDown = true;
        }
    }

    public void endCountDown(){
            Started = true;
            Player.GetComponent<PlaneMove>().enabled = true;
            CheckPointRender();
    }

    public void Crash()
    {
        if(Started)
        {
            Started = false;
            PlayerRb.velocity = Vector3.zero;
            PlayerRb.angularVelocity = Vector3.zero;
            Player.GetComponent<PlaneMove>().enabled = false;
            Text.GetComponent<TextMeshProUGUI>().SetText("Thumb Up to Restart");

            Player.transform.position = CheckpointManager.GetComponent<RaceTrackParse>().lastCheckPoint;
        }
    }

    public void setCheckPointsLeft(int cl)
    {
        checkPointsLeft = cl;
    }

    public void CheckPointRender()
    {
        string currCheckpointString = checkPointsLeft.ToString();
        Text.GetComponent<TextMeshProUGUI>().SetText("Checkpoints Left: " + currCheckpointString);
    }

    private void Update() {
        if(startCountDown && timelimit > 0)
        {
            timelimit -= Time.deltaTime;
            Player.GetComponent<PlaneMove>().enabled = false;
            Text.GetComponent<TextMeshProUGUI>().SetText(timelimit.ToString("F0"));
            Debug.Log(timelimit);
        }
        else if(startCountDown && timelimit <= 0)
        {
            endCountDown();
            timelimit = 3.0f;
            startCountDown = false;
            startStopwatch = true;
        }

        if(startStopwatch){
            startTime += Time.deltaTime;
            float minutes = Mathf.Floor(startTime / 60);
            float seconds = Mathf.Floor(startTime % 60);
            Stopwatch.GetComponent<TextMeshProUGUI>().SetText(minutes.ToString("00") + ":" + seconds.ToString("00"));
        }
    }

    public void EndGame(){
        Text.GetComponent<TextMeshProUGUI>().SetText("Course Comompleted!");
        startStopwatch = false;
        PlayerRb.velocity = Vector3.zero;
        PlayerRb.angularVelocity = Vector3.zero;
        Player.GetComponent<PlaneMove>().enabled = false;
        Time.timeScale = 0;
    }
}
