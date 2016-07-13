using UnityEngine;
using System.Collections;

public class Cannon001 : MonoBehaviour {

    public GameObject Bola;
    Transform CannonTrans;
    Transform BallSpawnPoint;
    float DeadZone;

	// Use this for initialization
    void Start()
    {
        DeadZone = .1f;
        CannonTrans = GameObject.FindGameObjectWithTag("Cannon").transform;
        BallSpawnPoint = GameObject.FindGameObjectWithTag("Ball Spawn Point").transform;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bola, BallSpawnPoint.position, Quaternion.identity);
        }

        if (Input.GetAxis("Horizontal") > DeadZone || Input.GetAxis("Horizontal") < -DeadZone)
        {
            CannonTrans.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0), Space.World);
        }

        if ((Input.GetAxis("Vertical") > DeadZone && CannonTrans.rotation.eulerAngles.x > 45) || (Input.GetAxis("Vertical") < -DeadZone && CannonTrans.localRotation.eulerAngles.x > 2))
        {
            CannonTrans.Rotate(new Vector3(Input.GetAxis("Vertical"), 0, 0));
        }



    }

}
