using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ladrillos
{
    struct Par
    {
        Vector3 Valor;
        GameObject ladrillo;
    }

    List<Par> Lista = new List<Par>();


}
public class Wall : MonoBehaviour, IWall {
    Dictionary<Vector3,GameObject> bricks;
    Vector3 area;
    Vector3 brickSize;
    GameObject freeBricks;
    Vector3 posicionGlobal;
    float expansionOnda; //Factor de propagación de la onda de impacto
    

	// Use this for initialization
	void Start ()
    {
        posicionGlobal = transform.position;
        bricks = new Dictionary<Vector3, GameObject>();
        GameObject Cube = new GameObject("Ladrillo");
        area = new Vector3(10, 40, 0.12f); //In meters.
        brickSize = new Vector3(0.25f, 0.05f, 0.12f);
        Material Madera = Resources.Load("Materials/Wood", typeof(Material)) as Material;
        expansionOnda = 40;

        freeBricks = new GameObject("Free Bricks");
        freeBricks.transform.parent = null;

        //Vector3 GlueSize = new Vector3(BrickSize.y, BrickSize.y, BrickSize.z);
        //Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //Cube.transform.parent = transform;
        //Cube.posicionGlobal = new Vector3(i + BrickSize.x / 2, j, k);
        //Cube.transform.localScale = GlueSize;
        //Glues.Add(Cube);



        for (float i = posicionGlobal.x - area.x * brickSize.x / 2; i < posicionGlobal.x + area.x * brickSize.x / 2; i += brickSize.x) // X = Ancho
        {
            for (float j = posicionGlobal.y + brickSize.y / 2; j < posicionGlobal.y + area.y * brickSize.y + brickSize.y / 2; j += brickSize.y) // Y = Altura
            {
                for (float k = posicionGlobal.z - area.z * brickSize.z / 2; k < posicionGlobal.z + area.z * brickSize.z / 2; k += brickSize.z) // Z = Profundo
                {
                    Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (Mathf.Floor(j) % brickSize.x == 0) Cube.transform.position = new Vector3(i - brickSize.x / 2, j, k);
                    else Cube.transform.position = new Vector3(i, j, k);
                    Cube.transform.parent = transform;
                    Cube.transform.localScale = brickSize;
                    Cube.GetComponent<MeshRenderer>().material = Madera;
                    Cube.tag = "Brick";
                    Cube.AddComponent<Brick>().Init(new Vector3(i, j, k),this);
                    
                    bricks.Add(new Vector3(i,j,k),Cube);

                }
            }
        } //Bricks generation



	}
    void DeleteBrick(GameObject ladrillo)
    {
        if (ladrillo != null)
        {
            ladrillo.transform.parent = freeBricks.transform;
            Rigidbody rigid = ladrillo.AddComponent<Rigidbody>();
            rigid.mass = 4;
            bricks[ladrillo.GetComponent<Brick>().pos] = null;
        }
    }
    void DeleteBrick(Vector3 posLadrillo)
    {
        if (bricks[posLadrillo] != null)
        {
            GameObject ladrillo = bricks[posLadrillo];
            ladrillo.transform.parent = freeBricks.transform;
            Rigidbody rigid = ladrillo.AddComponent<Rigidbody>();
            rigid.mass = 4;
            bricks[posLadrillo] = null;
        }
    }
    void CheckWallStructure()
    {
        // ToDo: Convertir ladrillos agrupados en paredes

        //Check empty rows

        bool flag;

        Debug.Log(posicionGlobal);
        for (float k = posicionGlobal.z - area.z * brickSize.z / 2; k < posicionGlobal.z + area.z * brickSize.z / 2; k += brickSize.z) // Z = Profundo
        {
            flag = true;
            for (float j = posicionGlobal.y + brickSize.y / 2; j < posicionGlobal.y + area.y * brickSize.y + brickSize.y / 2; j += brickSize.y)
            {
                
                for (float i = posicionGlobal.x - area.x * brickSize.x / 2; i < posicionGlobal.x + area.x * brickSize.x / 2; i += brickSize.x) // X = Ancho
                {
                    if (bricks[new Vector3(i,j,k)] != null)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    for (float i = posicionGlobal.x - area.x * brickSize.x / 2; i < posicionGlobal.x + area.x * brickSize.x / 2; i += brickSize.x) // X = Ancho
                    {
                        DeleteBrick(new Vector3(i, j, k));
                    }
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

        if (col.gameObject.tag == "Ball" || col.gameObject.tag == "Brick")
        {
            
            GameObject Ladrillo = col.contacts[0].thisCollider.gameObject;
            if (Ladrillo.GetComponents<Rigidbody>().Length == 0)
            {
                    PropagateImpact(Ladrillo, -col.impulse);
                                     
                        
                    CheckWallStructure();
                
            }
        }
    }

    public void CheckCollision(Vector3 posicion, Vector3 impulso)
    {

    }

    void PropagateImpact(GameObject ladrillo, Vector3 fuerza)
    {
        Vector3 posicion = ladrillo.GetComponent<Brick>().pos;
        if (fuerza.magnitude > 10)
        {
            ladrillo.transform.parent = freeBricks.transform;   
            Rigidbody rigid = ladrillo.AddComponent<Rigidbody>();
            rigid.mass = 4;
            bricks[posicion] = null;
            rigid.velocity = fuerza / 4;

            GameObject ladrilloContinuo;

            if (bricks.TryGetValue(posicion+new Vector3(brickSize.x,0,0),out ladrilloContinuo) && ladrilloContinuo != null)
            {
                PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.x);
            }

            if (bricks.TryGetValue(posicion + new Vector3(-brickSize.x, 0, 0), out ladrilloContinuo) && ladrilloContinuo != null)
            {
                PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.x);
            }

            if (bricks.TryGetValue(posicion + new Vector3(0,brickSize.y, 0), out ladrilloContinuo) && ladrilloContinuo != null)
            {
                PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.y);
            } 
            
            if (bricks.TryGetValue(posicion + new Vector3(0, -brickSize.y, 0), out ladrilloContinuo) && ladrilloContinuo != null)
            {
                PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.y);
            }
            
            if (bricks.TryGetValue(posicion + new Vector3(0,0, brickSize.z), out ladrilloContinuo) && ladrilloContinuo != null)
            {
                PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.z);
            }

            if (bricks.TryGetValue(posicion + new Vector3(0,0,-brickSize.z), out ladrilloContinuo) && ladrilloContinuo != null)
            {
                PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.z);
            }



            

        }

    }
    
}
