using UnityEngine;
using System.Collections;

public class WallBuilder : MonoBehaviour {

    public GameObject bigWall;
	// Use this for initialization
	void Start () {
        //Size brickSize = new Vector3(1.5f, 0.375f, 0.75f);    //Unidad ocupa 6*3*0.75   
        //Angulos Rectos += new Vector3(7.5,0,2.25) con  angulos de += Quaternion.Euler(0, 90, 0));
        Instantiate(bigWall, new Vector3(55, 0, 298), Quaternion.Euler(0, 0, 0));
        //Instantiate(bigWall, new Vector3(60, 0, 298), Quaternion.Euler(0, 0, 0));
        Instantiate(bigWall, new Vector3(62.5f, 0, 300.25f), Quaternion.Euler(0, 90, 0));
        Instantiate(bigWall, new Vector3(64.75f, 0, 292.75f), Quaternion.Euler(0, 180, 0));

        Instantiate(bigWall, new Vector3(64.75f, 0, 292.75f), Quaternion.Euler(0, 270, 0));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
