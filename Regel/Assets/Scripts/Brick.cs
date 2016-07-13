using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    Rigidbody rigid;
    public Vector3 pos;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}

    public void SetPos(Vector3 posicion)
    {
        pos = posicion;
    }
	
	// Update is called once per frame
	void Update () {

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
