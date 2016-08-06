using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Cannon001 : MonoBehaviour {

    
    public GameObject Bola;
    Transform CannonTrans;
    Vector3 BallSpawnPoint;
    float DeadZone;
    GameObject[] listaBolas;
    int maxBolas;             //Límite de bolas
    int indice;
	// Use this for initialization
    void Start()
    {
        maxBolas = 40;
        indice = 0;
        DeadZone = .1f;
        CannonTrans = transform;
        listaBolas = new GameObject[maxBolas]; 
        AgregarBola();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        BallSpawnPoint = transform.rotation * new Vector3(0, 0, 1) + transform.position;




    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int j = ObtenerBola();
            listaBolas[j].GetComponent<Ball>().SetExplosionPointAndReset(BallSpawnPoint + transform.rotation * new Vector3(0, 0, 0.2f));
            listaBolas[j].SetActive(true);
            listaBolas[j].transform.position = BallSpawnPoint;

        }

        if (Input.GetAxis("Horizontal") > DeadZone || Input.GetAxis("Horizontal") < -DeadZone)
        {
            CannonTrans.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0), Space.World);
        }

        if ((Input.GetAxis("Vertical") > DeadZone && CannonTrans.rotation.eulerAngles.x <= 75) || (Input.GetAxis("Vertical") < -DeadZone && CannonTrans.localRotation.eulerAngles.x >= 5))
        {
            CannonTrans.Rotate(new Vector3(Input.GetAxis("Vertical"), 0, 0));
        }
    }

    void AgregarBola()
    {
        listaBolas[indice] = (GameObject)Instantiate(Bola, BallSpawnPoint, Quaternion.identity);
        listaBolas[indice].SetActive(false);
    }

    int ObtenerBola()
    {
        for (int i = 0; i <= indice; i++)
        {
            if (listaBolas[i].activeSelf == false)
            {
                return i;
            }

        }
        if (indice < maxBolas)
        {

            indice++;
            AgregarBola();
            return 0;
        }
        else
        {
            indice = 1;
            AgregarBola();
            return 0;
        }
    }







    void OnDrawGizmosSelected()
    {
        // Show Ball Spawn Point
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(BallSpawnPoint, 0.05f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(BallSpawnPoint + transform.rotation * new Vector3(0, 0, 0.2f), 0.05f);
        
    }

}
