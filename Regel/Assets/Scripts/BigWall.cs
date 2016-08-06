using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BigWall : MonoBehaviour
{
    Vector3 area;
    Vector3 brickSize;
    float expansionOnda; //Factor de propagación de la onda de impacto


    // Use this for initialization
    void Start()
    {
        GameObject Cube = new GameObject("Ladrillo");
        area = new Vector3(8, 12, 0.5f); //In Bricks size.
        brickSize = new Vector3(1.25f, 0.25f, 1f);
        Material Madera = Resources.Load("Materials/Wood", typeof(Material)) as Material;
        expansionOnda = 40;


        //Vector3 GlueSize = new Vector3(BrickSize.y, BrickSize.y, BrickSize.z);
        //Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //Cube.transform.parent = transform;
        //Cube.transform.position = new Vector3(i + BrickSize.x / 2, j, k);
        //Cube.transform.localScale = GlueSize;
        //Glues.Add(Cube);



        //for (float i = transform.position.x - area.x * brickSize.x / 2; i < transform.position.x + area.x * brickSize.x / 2; i += brickSize.x) // X = Ancho
        //{
        //    for (float j = transform.position.y + brickSize.y / 2; j < transform.position.y + area.y * brickSize.y + brickSize.y / 2; j += brickSize.y) // Y = Altura
        //    {
        //        for (float k = transform.position.z - area.z * brickSize.z / 2; k < transform.position.z + area.z * brickSize.z / 2; k += brickSize.z) // Z = Profundo
        Vector3 posicion;
        for (float i = 0; i < area.x; i++) // X = Ancho
        {
            for (float j = 0; j < area.y; j++) // Y = Altura
            {
                for (float k = 0; k < area.z; k++) // Z = Profundo
                {
                    Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    posicion = new Vector3(transform.position.x - area.x * brickSize.x / 2 + i * brickSize.x, transform.position.y + brickSize.y / 2 + j * brickSize.y,
                                            transform.position.z - area.z * brickSize.z / 2 + k * brickSize.z);
                    Cube.transform.position = posicion;
                    //if (j % 2 == 0) Cube.transform.position += new Vector3(-brickSize.x / 2, 0, 0);
 
                    Cube.transform.position = posicion;
                    Cube.transform.parent = transform;
                    Cube.transform.localScale = brickSize;
                    Cube.GetComponent<MeshRenderer>().material = Madera;
                    Cube.tag = "Brick";
                    Cube.AddComponent<Rigidbody>().mass = 200 * brickSize.x * brickSize.y * brickSize.z;


                }
            }
        } //Bricks generation



    }
    // Update is called once per frame
    void Update()
    {

    }

}

