using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Projectile_Enemy_Shooting : MonoBehaviour
{
    public GameObject projectile;

    public float fireRate;
    public float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        fireDelay = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CanFireProjectile();
    }
    void CanFireProjectile()
    {
        if (Time.time > fireDelay)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            fireDelay = Time.time + fireRate;
        }
    }    
}
