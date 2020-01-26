using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector2 spawnPosition;
    public GameObject player;
    public GameObject winLvl;
    public int requiredStars;
    public float speed;
    public float requiredDistToWP = 0.05f;
    public Transform[] waypoints;

    private int curr = 0;
    private float currSpeed;
    private bool wasKilled;
    
    private PlayerController playerController;
    private WinLvlController winLvlController;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        winLvlController = winLvl.GetComponent<WinLvlController>();
    }

    void Update()
    {
        if (playerController.speed == 0)
        {
            currSpeed = 0;
        }
        else
        {
            if (playerController.hasMultiplier == true && wasKilled == false)
            {
                if (playerController.starCount < requiredStars)
                {
                    currSpeed = 0;
                }
                else
                {
                    currSpeed = 0.03f;
                }
            }
            else if (playerController.starCount < requiredStars || wasKilled == true)
            {
                currSpeed = 0;
                if (wasKilled == true)
                {
                    StartCoroutine(Wait(5, () =>
                    {
                        if (playerController.hasMultiplier)
                            currSpeed = 0.03f;
                        else
                            currSpeed = speed;

                        wasKilled = false;
                    }));
                }
            }
            else if (playerController.hasMultiplier == false && playerController.starCount >= requiredStars)
            {
                currSpeed = speed;
            }
        }
    }
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, waypoints[curr].position) > requiredDistToWP)
        {
            Vector2 move = Vector2.MoveTowards(transform.position, waypoints[curr].position, currSpeed);
            GetComponent<Rigidbody2D>().MovePosition(move);
        }
        else
        {
            curr = (curr + 1);
            if (curr == waypoints.Length)
                curr = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerController.hasMultiplier == true && collision.CompareTag("Player"))
        {
            transform.position = spawnPosition;
            curr = 0;
            wasKilled = true;
        }
    }

    IEnumerator Wait(float time, Action task)
    {
        yield return (new WaitForSeconds(5));
        task();
    }
}
