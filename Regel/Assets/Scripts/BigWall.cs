using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BigWall : MonoBehaviour, IWall
{
    Vector3 area;
    Vector3 brickSize;
    float expansionOnda; //Factor de propagación de la onda de impacto
    public PhysicMaterial materialFisico;
    Dictionary<Vector3, GameObject> bricks;

    void Awake()
    {
        GameObject Cube;
        brickSize = new Vector3(1.5f, 0.375f, 0.75f); //x = 2*z    
        area = new Vector3(4, 8, 1); //In Bricks size. //Unidad ocupa 6*3*0.75
        Material Madera = Resources.Load("Materials/Wood", typeof(Material)) as Material;
        expansionOnda = 2f; //Cuanto más chico, más expansion
        bricks = new Dictionary<Vector3, GameObject>();

        Vector3 posicion;        
        FixedJoint fixedJointBase;
        for (float i = 0; i < area.x; i++) // X = Ancho
        {
            for (float j = 0; j < area.y; j++) // Y = Altura
            {
                for (float k = 0; k < area.z; k++) // Z = Profundo
                {
                    Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Cube.transform.parent = transform;
                    posicion = new Vector3(area.x * brickSize.x / 2 + i * brickSize.x,brickSize.y / 2 + j * brickSize.y,
                                            area.z * brickSize.z / 2 + k * brickSize.z);
                    Cube.transform.localPosition = posicion;
                    if ((j % 2) == 0) Cube.transform.localPosition += new Vector3(-brickSize.x / 2, 0, 0);
                    Cube.transform.rotation = transform.rotation;
                    Cube.transform.localScale = brickSize;
                    Cube.GetComponent<BoxCollider>().material = materialFisico;
                    Cube.GetComponent<MeshRenderer>().material = Madera;
                    Cube.AddComponent<Rigidbody>().mass = 100 * brickSize.x * brickSize.y * brickSize.z;                    
                    bricks.Add(new Vector3(i, j, k), Cube);
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
                    if (k > 0)
                    {

                        fixedJointBase = bricks[new Vector3(i, j, k)].AddComponent<FixedJoint>();
                        fixedJointBase.connectedBody = bricks[new Vector3(i, j, k - 1)].GetComponent<Rigidbody>();
                        fixedJointBase.breakForce = 150;
                    }
                    Cube.AddComponent<Brick>().Init(new Vector3(i, j, k),this);


                }
            }
        } //Bricks generation

    }
    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

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

