using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public LogicScript logic;
    public float deadZone = 10;
    public float bulletSpeed = 5;
    public bool isPlayer = true;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.GetIsAllMoving())
        {
            if (isPlayer)
                MoveBullet(Vector3.up);

            else
                MoveBullet(Vector3.down);
        }
        
        else
            MoveBullet(Vector3.zero);
    }

    private void MoveBullet(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * bulletSpeed;

        if (Mathf.Abs(transform.position.y) >= deadZone)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isPlayer && col.gameObject.tag != "Player")
            Destroy(gameObject);

        if (!isPlayer && col.gameObject.tag != "Invader")
            Destroy(gameObject);
    }
}
