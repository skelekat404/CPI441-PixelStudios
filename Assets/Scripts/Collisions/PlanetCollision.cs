using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollision : MonoBehaviour
{
    public Rigidbody2D planet;

    public GameObject planetOneText;
    private bool planetOneBool;
    void Awake()
    {
        planet = GetComponent<Rigidbody2D>();

        planetOneText.SetActive(false);
        planetOneBool = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.tag.Equals("Player"))
        {
            planetOneText.SetActive(true);
            planetOneBool = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.tag.Equals("Player"))
        {
            planetOneText.SetActive(false);
            planetOneBool = false;
        }
    }
}
