using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {
    
	// Use this for initialization
    void Start()
    {
        gameObject.AddComponent<FixedJoint>();
        gameObject.AddComponent<FixedJoint>();
        gameObject.AddComponent<FixedJoint>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
