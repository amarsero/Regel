using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Wall : MonoBehaviour {
    Dictionary<Vector3,GameObject> Bricks;
    Vector3 Area;
    Vector3 BrickSize;

	// Use this for initialization
	void Start ()
    {
        Bricks = new Dictionary<Vector3, GameObject>();
        GameObject Cube = new GameObject();
        Area = new Vector3(10, 40, 0.3f); //In Bricks size
        BrickSize = new Vector3(0.25f, 0.05f, 0.12f);
        Material Madera = Resources.Load("Materials/Wood", typeof(Material)) as Material;

        //Vector3 GlueSize = new Vector3(BrickSize.y, BrickSize.y, BrickSize.z);
        //Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //Cube.transform.parent = transform;
        //Cube.transform.position = new Vector3(i + BrickSize.x / 2, j, k);
        //Cube.transform.localScale = GlueSize;
        //Glues.Add(Cube);

        float iini = transform.position.x - Area.x * BrickSize.x / 2; // i Inicial
        float jini = transform.position.y + BrickSize.y / 2;
        float kini = transform.position.z - Area.z * BrickSize.z / 2;

        for (float i = iini; i < transform.position.x + Area.x * BrickSize.x / 2; i += BrickSize.x) // X = Ancho
        {
            for (float j = jini; j < transform.position.y + Area.y * BrickSize.y + BrickSize.y / 2; j += BrickSize.y) //Y = Altura
            {
                for (float k =  kini; k < transform.position.z + Area.z * BrickSize.z / 2; k += BrickSize.z) // Z = Profundo
                {
                    Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Cube.transform.parent = transform;
                    if (Mathf.Floor(j) % BrickSize.x == 0) Cube.transform.position = new Vector3(i - BrickSize.x / 2, j, k);
                    else Cube.transform.position = new Vector3(i, j, k);
                    Cube.transform.localScale = BrickSize;
                    Cube.GetComponent<MeshRenderer>().material = Madera;
                    Cube.tag = "Brick";
                    Cube.AddComponent<Brick>().SetPos(new Vector3(i, j, k));
                    
                    Bricks.Add(new Vector3(i,j,k),Cube);

                }
            }
        } //Bricks generation



	}

    void CheckWallStructure()
    {

        for (int z = 0; z < Area.z; z++)
        {
            for (int y = 0; y < Area.y; y++)
            {
                
                if (true)
                {
                    
                }  
            }
        }
    }


	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ball")
        {
            for (int i = 0; i < col.contacts.Length; i++)
            {
                GameObject Ladrillo = col.contacts[i].thisCollider.gameObject;
                if (Ladrillo.GetComponents<Rigidbody>().Length == 0)
                {
                    PropagateImpact(Ladrillo, col.impulse);
                                     
                        
                    CheckWallStructure();
                }
            }
        }
    }

    void PropagateImpact(GameObject ladrillo, Vector3 fuerza)
    {
        Vector3 posicion = ladrillo.GetComponent<Brick>().pos;
        if (fuerza.magnitude > 10)
        {
            ladrillo.transform.parent = null;
            ladrillo.AddComponent<Rigidbody>().mass = 4;
            Bricks[posicion] = null;

     ///aplicar impulso y propagar a ladrillos lindantes

            

        }

    }
    
}
