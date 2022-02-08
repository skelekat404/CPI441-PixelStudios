using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep_AI : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float range;
    public float maxDist;

    Vector2 navigation;

    void Start()
    {
        SetNewNav();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, navigation, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, navigation) < range)
        {
            SetNewNav();
        }
    }

    void SetNewNav()
    {
        navigation = new Vector2(Random.Range(-maxDist, maxDist), Random.Range(-maxDist, maxDist));
    }
}
