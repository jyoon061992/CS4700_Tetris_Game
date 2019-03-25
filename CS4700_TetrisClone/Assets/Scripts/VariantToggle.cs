using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariantToggle : MonoBehaviour
{
    public GameObject leftpanel;
    public GameObject leftinversepanel;

    public void Toggle()
    {
        if(Camera.main.transform.eulerAngles == Vector3.zero)
            Camera.main.transform.eulerAngles = Vector3.zero + new Vector3(0, 0, 180);
        else
            Camera.main.transform.eulerAngles = Vector3.zero;


        leftinversepanel.SetActive(!leftinversepanel.activeSelf);
        leftpanel.SetActive(!leftpanel.activeSelf);

    }



}
