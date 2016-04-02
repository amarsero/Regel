using UnityEngine;
using System.Collections;

public class SMeta : MonoBehaviour
{

    GameObject Camera1;
    GameObject Camera2;
    // Use this for initialization
    void Start()
    {
        Camera1 = GameObject.Find("Camera1");
        Camera2 = GameObject.Find("Camera2");
        Camera2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {


            Camera1.SetActive(!Camera1.activeSelf);
            Camera2.SetActive(!Camera2.activeSelf);

        }
    }
}

