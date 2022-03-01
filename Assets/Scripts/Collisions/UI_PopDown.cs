using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopDown : MonoBehaviour
{
    public GameObject popup;

    public void PopUp()
    {
        if (popup == null)
        {
            popup.SetActive(false);
            print("not active");
        }
    }
}
