using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    public AudioSource source;
    public float timeToShoot = 1;
    private float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timer >= timeToShoot)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            source.Play();
            timer = 0;
        }
    }
}
