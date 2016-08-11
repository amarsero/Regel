using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brick : MonoBehaviour {

    Rigidbody rigid;
    public Vector3 pos;
    BigWall wallScript;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        wallScript = GameObject.FindGameObjectWithTag("Wall").GetComponent<BigWall>();
	}

    public void SetPos(Vector3 posicion)
    {
        pos = posicion;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision col)
    {
        if (col.impulse.magnitude > 10 && (col.gameObject.tag == "Ball" || col.gameObject.tag == "Brick"))
        {
            wallScript.CheckCollision(pos, col.impulse);
            
        }
    }
    


    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Ball")
    //    {
    //        rigid.isKinematic = false;
    //    }
    //    if (col.gameObject.tag == "Brick" & col.rigidbody.isKinematic == true)
    //    {            
    //        rigid.isKinematic = false;
    //    }
        
    //}



}
