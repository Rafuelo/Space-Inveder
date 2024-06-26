using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public LogicScript logic;
    public AudioSource source;
    public Animator animator;
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float boundaryX = 5f; // Adjust the boundary to limit movem

    private float timer = 0;

    void Update()
    {
        if (logic.GetIsAllMoving())
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

        else
            timer += Time.deltaTime;

        if (timer >= 2.5)
        {
            logic.SetIsAllMoving(true, animator);
            transform.position = new Vector2(-5.5f, -4.5f);
            timer = 0;
        }

        if (logic.isGameOver)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player Bullet")
        {
            logic.Removelive();
            logic.SetIsAllMoving(false, animator);
            source.Play();
        }
    }
}
