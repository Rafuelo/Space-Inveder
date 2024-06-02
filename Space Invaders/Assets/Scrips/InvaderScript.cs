using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderScript : MonoBehaviour
{
    public LogicScript logic;
    public InvadersScript invadersScript;
    public int deathScore;

    public Animator animator;
    public float deadTime = 0.1f;

    private void Update()
    {
        animator.SetBool("Is Move", invadersScript.GetIsMoved());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            animator.SetBool("Is Dead", true);
            invadersScript.InvaderDied();
            logic.AddScore(deathScore);
            Destroy(gameObject, deadTime);
        }
    }
}
