using UnityEngine;
using System.Collections;

public class Cannon001 : MonoBehaviour {

    public GameObject Bola;
    Transform CannonTrans;
    Vector3 BallSpawnPoint;
    float DeadZone;

	// Use this for initialization
    void Start()
    {
        DeadZone = .1f;
        CannonTrans = transform;
        
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        BallSpawnPoint = transform.rotation * new Vector3(0, 0, 1) + transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bola, BallSpawnPoint, Quaternion.identity);
        }

        if (Input.GetAxis("Horizontal") > DeadZone || Input.GetAxis("Horizontal") < -DeadZone)
        {
            CannonTrans.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0), Space.World);
        }

        if ((Input.GetAxis("Vertical") > DeadZone && CannonTrans.rotation.eulerAngles.x <= 75) || (Input.GetAxis("Vertical") < -DeadZone && CannonTrans.localRotation.eulerAngles.x >= 5 ))
        {
            CannonTrans.Rotate(new Vector3(Input.GetAxis("Vertical"), 0, 0));
        }



    }

    //To Do:
    //Implementar sistema para guardar Bolas en un array y moverlas al BallSpawnPoint en vez de instanciarlas en el momento








    void OnDrawGizmosSelected()
    {
        // Show Ball Spawn Point
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(BallSpawnPoint, 0.05f);
    }

}
