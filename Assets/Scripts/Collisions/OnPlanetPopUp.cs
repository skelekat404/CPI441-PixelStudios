using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanetPopUp : MonoBehaviour
{
    public Rigidbody2D shipPop;

    public GameObject shipPopText;

    private bool shipPopBool;

    void Awake()
    {
        shipPop = GetComponent<Rigidbody2D>();

        shipPopText.SetActive(false);
        shipPopBool = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.tag.Equals("Player"))
        {
            Debug.Log("Player colliding with ship >:)");

            shipPopText.SetActive(true);
            shipPopBool = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.root.tag.Equals("Player"))
        {
            shipPopText.SetActive(false);
            shipPopBool = false;
        }
    }
}
