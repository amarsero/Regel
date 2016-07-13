using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LineaMira : MonoBehaviour {

    Transform Cannon;
    LineRenderer Linea;
    Transform Boca;
    Vector3 Squirly; //Rotación de espiral
    Quaternion rot; //Rotación de caída
    int VertexCount;
    int Longitud;
	// Use this for initialization
	void Start () 
    {
        VertexCount = 100;
        Longitud = 4;
        Cannon = GameObject.FindWithTag("Cannon").transform;
        Linea = GetComponent<LineRenderer>();
        Linea.SetVertexCount(VertexCount);
        var children = Cannon.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child.name == "Mouth")
                Boca = child;
                
	}
	
	// Update is called once per frame
    //void Update () 
    //{
    //    Quaternion rot = Cannon.rotation * Quaternion.Euler(-0.5f, 0, 0);
    //    Vector3 pos = Boca.position + new Vector3(0,1,-0.3f);
    //    for (int i = 0; i < 100; i++)
    //    {
    //        Squirly = new Vector3();
    //        Squirly.x = i * Mathf.Sin(Mathf.Deg2Rad*(i*10));
    //        Squirly.y = i * Mathf.Cos(Mathf.Deg2Rad*(i*10));
    //        Squirly.z = i;
    //        Linea.SetPosition(i, pos + Quaternion.Euler(Squirly) * Vector3.down * i);
    //        pos = pos + rot *  Vector3.down * 5;
    //        rot = rot;//* Quaternion.Euler(-0.92f, 0, 0);



    //        //Linea.SetPosition(i, Boca.position + (Quaternion.Euler(Squirly) * Vector3.down * 1/i) + rot * Vector3.down);
    //        //rot = rot * Quaternion.Euler(-0.5f, 0, 0);


    //    }

    //}

    void Update()
    {
        Squirly = new Vector3();
        rot = Cannon.rotation * Quaternion.Euler(0f, 0, 0);
        for (int i = 0; i < VertexCount; i++)
                {
            //    Squirly.y = 4 * Mathf.Sin(Mathf.Deg2Rad * i * 30f);
            //    Squirly.x = 4 * Mathf.Cos(Mathf.Deg2Rad * i * 30f);
                rot = rot * Quaternion.Euler((Longitud/4) * -0.45f * (1.07f-Cannon.localRotation.x), 0, 0);
                Linea.SetPosition(i, Boca.position + Quaternion.Euler(Squirly) * rot * Vector3.down * i * Longitud);
			}
    }
}
