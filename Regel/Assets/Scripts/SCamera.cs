using UnityEngine;
using System.Collections;

public class SCamera : MonoBehaviour
{
    Transform Boca;
    Transform Cannon;
    void Start()
    {
        Cannon = GameObject.FindGameObjectWithTag("Cannon").transform;
         var   children = Cannon.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child.name == "Mouth")
                Boca = child;
	}
	
	// Update is called once per frame
    void Update()
    {
        //transform.position = Boca.position + new Vector3(3,2,-20);
        //transform.rotation = Cannon.rotation * Quaternion.Euler(90, 180, 0) * Quaternion.Euler(Cannon.eulerAngles.x / 4,0,0);
        //    //restar mitad rotación local cannon0001


    }
 
}
