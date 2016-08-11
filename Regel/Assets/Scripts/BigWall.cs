using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BigWall : MonoBehaviour
{
    Vector3 area;
    Vector3 brickSize;
    float expansionOnda; //Factor de propagación de la onda de impacto
    public PhysicMaterial materialFisico;
    Dictionary<Vector3, GameObject> bricks;

    void Awake()
    {
        GameObject Cube = new GameObject("Ladrillo");
        brickSize = new Vector3(1.2f, 0.35f, 0.7f);        
        area = new Vector3(10, 12, 1); //In Bricks size.
        Material Madera = Resources.Load("Materials/Wood", typeof(Material)) as Material;
        expansionOnda = 2f; //Cuanto más chico, más expansion
        bricks = new Dictionary<Vector3, GameObject>();

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
                    if ((j % 2) == 0) Cube.transform.position += new Vector3(-brickSize.x / 2, 0, 0);
                    Cube.transform.parent = transform;
                    Cube.transform.localScale = brickSize;
                    Cube.GetComponent<BoxCollider>().material = materialFisico;
                    Cube.GetComponent<MeshRenderer>().material = Madera;
                    Cube.tag = "Brick";
                    Cube.AddComponent<Rigidbody>().mass = 100 * brickSize.x * brickSize.y * brickSize.z;
                    Cube.AddComponent<Brick>().SetPos(new Vector3(i, j, k));
                    bricks.Add(new Vector3(i, j, k), Cube);


                }
            }
        } //Bricks generation

    }
    // Use this for initialization
    void Start()
    {
       
        FixedJoint fixedJointBase;

        for (float i = 0; i < area.x; i++) // X = Ancho
        {
            for (float j = 0; j < area.y; j++) // Y = Altura
            {
                for (float k = 0; k < area.z; k++) // Z = Profundo
                {
                    if (i > 0)
                    {
                        fixedJointBase = bricks[new Vector3(i, j, k)].AddComponent<FixedJoint>();
                        fixedJointBase.connectedBody = bricks[new Vector3(i - 1, j, k)].GetComponent<Rigidbody>();
                        fixedJointBase.breakForce = 800; //Esto en 10000 crea filas

                    }
                    if(j > 0)
                    {
                        fixedJointBase = bricks[new Vector3(i, j, k)].AddComponent<FixedJoint>();
                        fixedJointBase.connectedBody = bricks[new Vector3(i, j - 1, k)].GetComponent<Rigidbody>();
                        fixedJointBase.breakForce = 1000; //Esto en 10000 crea columnas
                    }
                    if(k > 0)
                    {

                        fixedJointBase = bricks[new Vector3(i, j, k)].AddComponent<FixedJoint>();
                        fixedJointBase.connectedBody = bricks[new Vector3(i , j, k - 1)].GetComponent<Rigidbody>();
                        fixedJointBase.breakForce = 150; //meh

                    }
                }
            }
        }



    }
    // Update is called once per frame
    void Update()
    {

    }
    // void OnCollisionEnter(Collision col)
    //{

    //    if (col.gameObject.tag == "Ball" || col.gameObject.tag == "Brick")
    //    {
    //        GameObject ladrillo = col.contacts[0].thisCollider.gameObject;
    //        if (bricks[ladrillo.GetComponent<Brick>().pos] != null)
    //        {
    //                PropagateImpact(ladrillo, -col.impulse);
                                     

                
    //        }
    //    }
    //}
    public void CheckCollision(Vector3 posicion, Vector3 impulso)
    {
        if (bricks[posicion] != null && impulso.magnitude > 10)
        {
            PropagateImpact(posicion, -impulso);
        }
    }
     public void PropagateImpact(GameObject ladrillo, Vector3 fuerza)
     {
         Vector3 posicion = ladrillo.GetComponent<Brick>().pos;
         bricks[posicion] = null;
         _PropagateImpact(posicion, fuerza);

     }
     public void PropagateImpact(Vector3 posicion, Vector3 fuerza)
     {
         bricks[posicion] = null;
         _PropagateImpact(posicion, fuerza);

     }

    private void _PropagateImpact(Vector3 posicion, Vector3 fuerza)
    {

         if (fuerza.magnitude > 5)
         {
             GameObject ladrilloContinuo;

             if (posicion.x < area.x - 1)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(1, 0, 0)];
                 if (ladrilloContinuo != null)
                 {
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.x);
                 }
             }
             if(posicion.x > 0)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(-1, 0, 0)];
                 if (ladrilloContinuo != null)
                 {
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.x);
                 }
             }

             if (posicion.y < area.y - 1)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(0, 1, 0)];
                 if (ladrilloContinuo != null)
                 {
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.y);
                 }
             }
             if (posicion.y > 0)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(0, -1, 0)];
                 if (ladrilloContinuo != null)
                 {
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.y);
                 }
             }

             if (posicion.z < area.z - 1)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(0, 0, 1)];
                 if (ladrilloContinuo != null)
                 {
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.z);
                 }
             }
             if (posicion.z > 0)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(0, 0, -1)];
                 if (ladrilloContinuo != null)
                 {
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / brickSize.z);
                 }
             }




         }
     }
}

