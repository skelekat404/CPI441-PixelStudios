using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_HealthBar : MonoBehaviour
{
    private Transform bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
    public void SetColor(Color barColor)
    {
        bar.Find("Bar_Sprite").GetComponent<SpriteRenderer>().color = barColor;
    }
}
