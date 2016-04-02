using UnityEngine;
using System.Collections;

public class Cannon001 : MonoBehaviour {

    public GameObject Bolas;
    Transform CannonTrans;
    Vector3 BocaCannon;
	// Use this for initialization
    void Start()
    {
        CannonTrans = GameObject.FindGameObjectWithTag("Cannon").transform;
        BocaCannon = GameObject.Find("Cannon001/Mouth").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(Bolas, BocaCannon, Quaternion.identity);


        }
	}
}
