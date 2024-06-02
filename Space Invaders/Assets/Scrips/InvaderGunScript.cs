using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderGunScript : MonoBehaviour
{

    public GameObject bullet;
    public float lowestTime = 2;
    public float highestTime = 10;
    private float timeToShoot;
    private float timer = 0;

    private void Start()
    {
        timeToShoot = Random.Range(lowestTime, highestTime);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToShoot)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            timer = 0;
        }
    }
}
