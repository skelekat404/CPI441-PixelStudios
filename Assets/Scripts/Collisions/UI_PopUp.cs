using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUp : MonoBehaviour
{
    public GameObject popup;

    public void PopUp()
    {
        if(popup != null)
        {
            popup.SetActive(true);
            print("active");
        }
    }
}
