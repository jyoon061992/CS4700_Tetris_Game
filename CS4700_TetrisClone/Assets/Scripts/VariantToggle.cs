using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariantToggle : MonoBehaviour
{
    public GameObject leftpanel;
    public GameObject leftinversepanel;
    private AudioSource aS;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();
        aS.volume = 0;
    }

    public void Toggle()
    {
        if(Camera.main.transform.eulerAngles == Vector3.zero)
            Camera.main.transform.eulerAngles = Vector3.zero + new Vector3(0, 0, 180);
        else
            Camera.main.transform.eulerAngles = Vector3.zero;


        leftinversepanel.SetActive(!leftinversepanel.activeSelf);
        leftpanel.SetActive(!leftpanel.activeSelf);

        if (aS.volume == 0)
            aS.volume = 1;
        else
            aS.volume = 0;
    }



}
