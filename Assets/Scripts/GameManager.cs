using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool Started = false;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        UI = GameObject.FindGameObjectsWithTag("UI")[0];
        Text = GameObject.FindGameObjectsWithTag("UI")[1];

        Player.GetComponent<PlaneMove>().enabled = false;
    }

    public void StartGame()
    {
        if(!Started)
        {
            Started = true;
            Player.GetComponent<PlaneMove>().enabled = true;
            Text.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void EndGame()
    {
        if(Started)
        {
            Started = false;
            Player.GetComponent<PlaneMove>().enabled = false;
            Text.GetComponent<TextMeshProUGUI>().text = "Game Over, Pinch to Restart";
        }
    }
}
