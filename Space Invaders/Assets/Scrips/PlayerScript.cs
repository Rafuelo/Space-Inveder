using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public LogicScript logic;
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float boundaryX = 5f; // Adjust the boundary to limit movem

    void Update()
    {
        // Calculate movement direction based on input
        float moveDirection = Input.GetAxis("Horizontal");

        // Move the object horizontally
        transform.Translate(Vector3.right * moveDirection * moveSpeed * Time.deltaTime);

        // Clamp the position to stay within boundaries
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -boundaryX, boundaryX);
        transform.position = currentPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.Removelive();
    }
}
