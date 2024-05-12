using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool Started = false;
    public string MaxCheckpoints;
    private int count = 1;

    [SerializeField] private GameObject CheckpointManager;
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody PlayerRb;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        CheckpointManager = GameObject.FindGameObjectsWithTag("CheckPointManager")[0];
        PlayerRb = Player.GetComponent<Rigidbody>();
        MaxCheckpoints = CheckpointManager.GetComponent<RaceTrackParse>().checkpoints.Count.ToString();

        Text.GetComponent<TextMeshProUGUI>().SetText("Thumb Up to Start");

        Player.GetComponent<PlaneMove>().enabled = false;
    }

    public void StartGame()
    {
        if(!Started)
        {
            Started = true;
            Player.GetComponent<PlaneMove>().enabled = true;
            Text.GetComponent<TextMeshProUGUI>().SetText("Checkpoints: 0/" + MaxCheckpoints);
        }
    }

    public void Crash()
    {
        if(Started)
        {
            Debug.Log("End Game");
            Started = false;
            PlayerRb.velocity = Vector3.zero;
            PlayerRb.angularVelocity = Vector3.zero;
            Player.GetComponent<PlaneMove>().enabled = false;
            Text.GetComponent<TextMeshProUGUI>().SetText("Thumb Up to Restart");

            Player.transform.position = CheckpointManager.GetComponent<RaceTrackParse>().lastCheckPoint;
        }
    }

    public void CheckPointReached(){
        string strcount = count.ToString();
        Text.GetComponent<TextMeshProUGUI>().SetText("Checkpoints:" + strcount + "/" + MaxCheckpoints);
        count++;
    }
}
