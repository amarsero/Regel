using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IWall
{
    // Property signatures:
    void CheckCollision(Vector3 posicion, Vector3 impulso);

}

public class Brick : MonoBehaviour {

    Rigidbody rigid;
    public Vector3 pos;
    IWall wallScript;


	// Use this for initialization
	void Start () {
	}

    public void Init(Vector3 posicion,IWall script)
    {
        pos = posicion;
        wallScript = script;
        rigid = GetComponent<Rigidbody>();
        gameObject.tag = "Brick";
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
