using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylerCollsionManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject GameManager;

    private void Start() {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        rb = Player.GetComponent<Rigidbody>();
        GameManager = GameObject.FindGameObjectsWithTag("GameManager")[0];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
          //Debug.Log("Game Over");
          GameManager.GetComponent<GameManager>().EndGame();  
        }
    }
}
