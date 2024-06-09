using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderGunScript : MonoBehaviour
{
    public LogicScript logic;
    public GameObject bullet;
    public float lowestTime = 2;
    public float highestTime = 10;
    private float timeToShoot;
    private float timer = 0;

    private void Start()
    {
        RadomaisNumber();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToShoot && logic.GetIsAllMoving())
        {
            Instantiate(bullet, transform.position, transform.rotation);
            RadomaisNumber();
            timer = 0;
        }
    }

    private void RadomaisNumber()
    {
        timeToShoot = Random.Range(lowestTime, highestTime);
    }
}
