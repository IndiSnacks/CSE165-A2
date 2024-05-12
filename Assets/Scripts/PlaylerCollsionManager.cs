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
          //rb.velocity = Vector3.zero;
          GameManager.GetComponent<GameManager>().EndGame();  
        }
    }
}
