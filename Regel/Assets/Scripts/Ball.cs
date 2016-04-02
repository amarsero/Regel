using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    Rigidbody rigid;
    Transform CannonTrans;
    bool Fire;
    GameObject Cube;

	// Use this for initialization
	void Start () 
    {
        rigid = GetComponent<Rigidbody>();

        CannonTrans = GameObject.FindGameObjectWithTag("Cannon").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire = true;
        }
        if (Fire == true)
        {
            rigid.AddExplosionForce(3000 * 5 * 10, CannonTrans.position + CannonTrans.rotation * new Vector3(0, 1, 0), 5);
            if (Cube != null)
            {
                Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Cube.transform.position = CannonTrans.position + CannonTrans.rotation * new Vector3(0, 1, 0);
                Cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
        
	}


    
}
