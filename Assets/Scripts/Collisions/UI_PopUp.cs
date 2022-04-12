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
            bool active = popup.activeSelf;

            popup.SetActive(!active);

            print("active");
        }
    }
}
