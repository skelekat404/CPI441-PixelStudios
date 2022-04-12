using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Goober_AI : MonoBehaviour
{
    public GameObject projectile;

    public float fireRate = 0.5f;
    public float fireDelay;
    public bool canFireProjectile;

    // Start is called before the first frame update
    void Start()
    {
        //fireRate = 2f;
        fireDelay = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFireProjectile)
        {
            FireProjectile();
        }

    }
    void FireProjectile()
    {
        if (Time.time > fireDelay)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            fireDelay = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ship"))
        {
            canFireProjectile = true;

            Debug.Log("Can fire projectile");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ship"))
        {
            canFireProjectile = false;
            Debug.Log("Can NOT fire projectile");
        }
    }

}
