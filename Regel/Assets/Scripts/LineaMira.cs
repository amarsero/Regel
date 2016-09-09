using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LineaMira : MonoBehaviour {

    LineRenderer Linea;
    Vector3 Squirly; //Rotación de espiral
    Quaternion rot; //Rotación de caída
    int VertexCount;
    int Longitud;
    Vector3 targetPosition;
	// Use this for initialization
	void Start () 
    {
        Longitud = 4;
        Linea = GetComponent<LineRenderer>();
        Linea.SetVertexCount(VertexCount);

        Color start = new Color(0, 200/255f, 50/255f);
        Color end = new Color(0, 230 / 255f, 0, 0.5f);
        Linea.SetColors(start, end);
                
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

        VertexCount = 100;
        Linea.SetVertexCount(VertexCount);
        for (int i = 0; i < VertexCount; i++)
        {
            //Squirly.y =  2 * Mathf.Sin(Mathf.Deg2Rad * i * 30f);
            //Squirly.x =  2 * Mathf.Cos(Mathf.Deg2Rad * i * 30f);
            targetPosition = Quaternion.Euler(i / 10f, 0, 0) * Vector3.forward *  i/5;
            Linea.SetPosition(i, targetPosition);
            if (Physics.CheckSphere(transform.position + transform.rotation * (targetPosition + new Vector3(0, 0, 0.15f)), 0.05f))
	        {
                VertexCount = i;
                

                Linea.SetVertexCount(VertexCount);
	        }
            
            
        }




        //Squirly = new Vector3();
        //rot = Cannon.rotation * Quaternion.Euler(0f, 0, 0);
        //for (int i = 0; i < VertexCount; i++)
        //        {
        //    //    Squirly.y = 4 * Mathf.Sin(Mathf.Deg2Rad * i * 30f);
        //    //    Squirly.x = 4 * Mathf.Cos(Mathf.Deg2Rad * i * 30f);
        //        rot = rot * Quaternion.Euler((Longitud/4) * -0.45f * (1.07f-Cannon.localRotation.x), 0, 0);
        //        Linea.SetPosition(i, Boca + Quaternion.Euler(Squirly) * rot * Vector3.down * i * Longitud);
        //    }
    }
    void OnDrawGizmosSelected()
    {
    }
}
