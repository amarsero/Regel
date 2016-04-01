using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    Rigidbody rigid;
    Transform CannonTrans;

	// Use this for initialization
	void Start () 
    {
        rigid = GetComponent<Rigidbody>();
        CannonTrans = GameObject.FindGameObjectWithTag("Cannon").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	    if (Time.time < 23)
        {
            rigid.AddExplosionForce(300, new Vector3(0, -1, -1), 5);
        }
	}
}
