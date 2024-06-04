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

    public float moveSpeed = 5f; // Adjust the speed as needed
    public float boundaryX = 5f; // Adjust the boundary to limit movem
    public float moveInterval = 0.7f; // Adjust the interval between moves
    private float timer = 0;
    private Vector2 currentDirection = Vector2.left;

    private bool movedDown = false;
    private bool isMoved = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= moveInterval && logic.GetIsAllMoving())
        {
            timer = 0;

            if (Mathf.Abs(transform.position.x) >= 2 && !movedDown)
            {
                Move(Vector2.down);
                currentDirection *= -1;
                movedDown = true;
            }

            else
            {
                Move(currentDirection);
                movedDown = false;
            }

            clipCounter++;

            if (clipCounter > 4)
                clipCounter = 1;
        }

        if (sumOfInvaders == 0)
        {
            logic.AddLive();
            logic.StartAgain();
        }

        if (logic.isGameOver)
            gameObject.SetActive(false);
    }

    void Move(Vector2 moveDirection)
    {
        // Calculate the new position
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed;

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
