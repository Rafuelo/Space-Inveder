using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvadersScript : MonoBehaviour
{
    public LogicScript logic;
    public AudioSource source, deathSource;
    public AudioClip invaderStepClip1, invaderStepClip2, invaderStepClip3, invaderStepClip4;
    private int clipCounter = 1;
    public int sumOfInvaders = 55;
    public float moveInterval = 0.1f; 
    public float boundaryX = 2f; 
    public float moveSpeed = 0.7f; 
    private float timer = 0;
    private Vector2 currentDirection = Vector2.right;
    private bool movedDown = false;
    private bool isMoved = false;
    private int previousInvaderCount; // Tracks invader count for speed adjustment
    public int decreaseInterval = 5; // Interval at which speed decreases


    private void Start()
    {
        previousInvaderCount = sumOfInvaders;

        // Cap the wave number at 10 for speed and position calculations
        int effectiveWave = Mathf.Min(logic.GetWave(), 10);

        // Only adjust speed and position every 2 waves
        if (effectiveWave % 2 == 0)
        {
            // Calculate the move speed based on the effective wave
            moveSpeed -= -0.05f * (effectiveWave / 2);

            // Calculate the Y position based on the effective wave
            float yPos = transform.position.y - (effectiveWave / 2) * -0.2f;

            // Set the initial position of the invader
            Vector3 startPosition = new Vector3(transform.position.x, yPos, transform.position.z);
            transform.position = startPosition;

        }
        else
        {
            // If the wave is not changing speed and position, use the last calculated values
            moveSpeed -= -0.05f * ((effectiveWave - 1) / 2);
            float yPos = transform.position.y - (effectiveWave / 2) * -0.2f;

            Vector3 startPosition = new Vector3(transform.position.x, yPos, transform.position.z);
            transform.position = startPosition;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= moveSpeed && logic.GetIsAllMoving())
        {
            timer = 0;

            if (Mathf.Abs(transform.position.x) >= 2 && !movedDown)
            {
                Move(Vector2.down, moveInterval * 2);
                currentDirection *= -1;
                RemoveSpeed(0.05f);
                movedDown = true;
            }

            else
            {
                Move(currentDirection, moveInterval);
                movedDown = false;
            }

            if ((previousInvaderCount - sumOfInvaders) >= decreaseInterval)
            {
                RemoveSpeed(0.02f); // Decrease moveSpeed by 0.02
                previousInvaderCount = sumOfInvaders; // Update previousInvaderCount
            }

            clipCounter++;

            if (clipCounter > 4)
                clipCounter = 1;
        }


        if (sumOfInvaders == 0)
        {
            logic.AddLive();
            logic.AddWave();
            logic.StartAgain();
        }

        if (logic.isGameOver)
            gameObject.SetActive(false);
    }

    void Move(Vector2 moveDirection, float moveInterval)
    {
        // Calculate the new position
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveInterval;

        // Move the enemy to the new position
        transform.position = newPosition;

        if (isMoved)
            isMoved = false;

        else
            isMoved = true;

        switch (clipCounter)
        {
            case 1:
                source.clip = invaderStepClip1;
                break;

            case 2:
                source.clip = invaderStepClip2;
                break;

            case 3:
                source.clip = invaderStepClip3;
                break;

            case 4:
                source.clip = invaderStepClip4;
                break;
        }
        source.Play();
    }

    private void RemoveSpeed(float decreaseAmount)
    {
        moveSpeed = Mathf.Max(0.1f, moveSpeed - decreaseAmount); // Ensure moveSpeed doesn't go below 0
    }
    public bool GetIsMoved()
    {
        return isMoved;
    }
    public void InvaderDied()
    {
        sumOfInvaders--;
        deathSource.Play();
    }
}
