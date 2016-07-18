﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Wall : MonoBehaviour {
    Dictionary<Vector3,GameObject> bricks;
    Vector3 area;
    Vector3 brickSize;
    GameObject freeBricks;

    float expansionOnda; //Factor de propagación de la onda de impacto

	// Use this for initialization
	void Start ()
    {
        bricks = new Dictionary<Vector3, GameObject>();
        GameObject Cube = new GameObject("Kadrukki");
        area = new Vector3(10, 40, 0.12f); //In Bricks size
        brickSize = new Vector3(0.25f, 0.05f, 0.12f);
        Material Madera = Resources.Load("Materials/Wood", typeof(Material)) as Material;
        expansionOnda = 40;

        freeBricks = new GameObject("Free Bricks");
        freeBricks.transform.parent = null;

        //Vector3 GlueSize = new Vector3(BrickSize.y, BrickSize.y, BrickSize.z);
        //Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //Cube.transform.parent = transform;
        //Cube.transform.position = new Vector3(i + BrickSize.x / 2, j, k);
        //Cube.transform.localScale = GlueSize;
        //Glues.Add(Cube);



        for (float i = transform.position.x - area.x * brickSize.x / 2; i < transform.position.x + area.x * brickSize.x / 2; i += brickSize.x) // X = Ancho
        {
            for (float j = transform.position.y + brickSize.y / 2; j < transform.position.y + area.y * brickSize.y + brickSize.y / 2; j += brickSize.y) //Y = Altura
            {
                for (float k = transform.position.z - area.z * brickSize.z / 2; k < transform.position.z + area.z * brickSize.z / 2; k += brickSize.z) // Z = Profundo
                {
                    Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (Mathf.Floor(j) % brickSize.x == 0) Cube.transform.position = new Vector3(i - brickSize.x / 2, j, k);
                    else Cube.transform.position = new Vector3(i, j, k);
                    Cube.transform.parent = transform;
                    Cube.transform.localScale = brickSize;
                    Cube.GetComponent<MeshRenderer>().material = Madera;
                    Cube.tag = "Brick";
                    Cube.AddComponent<Brick>().SetPos(new Vector3(i, j, k));
                    
                    bricks.Add(new Vector3(i,j,k),Cube);

                }
            }
        } //Bricks generation

	}

    void CheckWallStructure()
    {

        for (int z = 0; z < area.z; z++)
        {
            for (int y = 0; y < area.y; y++)
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

    void PropagateImpact(GameObject ladrillo, Vector3 fuerza)
    {
        Vector3 posicion = ladrillo.GetComponent<Brick>().pos;
        if (fuerza.magnitude > 10)
        {
            ladrillo.transform.parent = freeBricks.transform;   
            Rigidbody rigid = ladrillo.AddComponent<Rigidbody>();
            rigid.mass = 4;
            rigid.velocity = fuerza / 4;
            bricks[posicion] = null;

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



     ///aplicar impulso y propagar a ladrillos lindantes

            

        }

    }
    
}
