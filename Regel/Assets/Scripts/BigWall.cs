using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ToDo:
// Static public Poder crear ladrillos individuales
// Arreglar (o Balancear) PropagateImpact Nota: Arreglar Distancia
// Agregar columnas fijas al suelo?

public class BigWall : MonoBehaviour, IWall
{
    Vector3 area;
    static Vector3 _brickSize;
    float expansionOnda; //Factor de propagación de la onda de impacto
    public PhysicMaterial materialFisico;
    Dictionary<Vector3, GameObject> bricks;

    static public Vector3 brickSize
    {
        get
        {
            return _brickSize;
        }
        private set
        { 
            _brickSize = value;
        }


    }
    void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Crea la pared que se va a representar en el juego
    /// </summary>
    /// <param name="doble">Doble longitud?</param>
    /// 
    public void CrearColumna()
    {
        GameObject Cube;
        Material Madera = Resources.Load("Materials/Wood", typeof(Material)) as Material;
        FixedJoint fixedJointBase;
        bricks = new Dictionary<Vector3, GameObject>();

        _brickSize = new Vector3(1.5f, 0.375f, 0.75f); //x = 2*z = 4*y    
        area = new Vector3(1, 8, 1); //In Bricks size. //Unidad ocupa 6*3*0.75(precario)

        for (int i = 0; i < area.y; i++)
        {
              Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
              Cube.transform.localScale = _brickSize + new Vector3(-_brickSize.z,0,0);
              Cube.transform.position = new Vector3(0, _brickSize.y / 2 + i * _brickSize.y, 0);
              Cube.transform.parent = transform;
              Cube.transform.rotation = transform.rotation;
              Cube.GetComponent<BoxCollider>().material = materialFisico;
              Cube.GetComponent<MeshRenderer>().material = Madera;
              Cube.AddComponent<Rigidbody>().mass = 100 * _brickSize.x * _brickSize.y * _brickSize.z;
              bricks.Add(new Vector3(0, i, 0), Cube);
              if (i > 0)
              {
                  fixedJointBase = bricks[new Vector3(0, i, 0)].AddComponent<FixedJoint>();
                  fixedJointBase.connectedBody = bricks[new Vector3(0, i - 1, 0)].GetComponent<Rigidbody>();
                  fixedJointBase.breakForce = 800; //Esto en 10000 crea filas

              }
        }


    }

    public void CrearPared(bool doble)
    {
        //Variables temporales
        GameObject Cube;
        Vector3 posicion;
        Material Madera = Resources.Load("Materials/Wood", typeof(Material)) as Material;
        FixedJoint fixedJointBase;

        //Definición de variables de la clase globales
        _brickSize = new Vector3(1.5f, 0.375f, 0.75f); //x = 2*z = 4*y    
        area = new Vector3(4, 8, 1); //In Bricks size. //Unidad ocupa 6*3*0.75(precario)
        if (doble) area.x *= 2;
        expansionOnda = 4; //Cuanto más chico, más expansion
        bricks = new Dictionary<Vector3, GameObject>();


        for (float i = 0; i < area.x; i++) // X = Ancho
        {
            for (float j = 0; j < area.y; j++) // Y = Altura
            {
                for (float k = 0; k < area.z; k++) // Z = Profundo
                {
                    Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Cube.transform.localScale = _brickSize;
                    Cube.transform.parent = transform;
                    posicion = new Vector3(-area.x * _brickSize.x / 2 + _brickSize.x * 3/ 4 + i * _brickSize.x, _brickSize.y / 2 + j * _brickSize.y,
                                            -area.z * _brickSize.z / 2 + _brickSize.z / 2 + k * _brickSize.z);
                    Cube.transform.localPosition = posicion;

                    if ((j % 2) == 0)
                    {
                        Cube.transform.localPosition += new Vector3(-_brickSize.z, 0, 0);
                        if (i == 0)
                        {
                            Cube.transform.localScale  += new Vector3(-_brickSize.z, 0, 0);
                            Cube.transform.localPosition += new Vector3(_brickSize.y, 0, 0);
                        }

                    }

                    else if (i == (area.x - 1))
                    {
                        Cube.transform.localScale += new Vector3(-_brickSize.z, 0, 0);
                        Cube.transform.localPosition += new Vector3(-_brickSize.y, 0, 0);
                    }


                    Cube.transform.rotation = transform.rotation;
                    Cube.GetComponent<BoxCollider>().material = materialFisico;
                    Cube.GetComponent<MeshRenderer>().material = Madera;
                    Cube.AddComponent<Rigidbody>().mass = 100 * _brickSize.x * _brickSize.y * _brickSize.z;    
                    bricks.Add(new Vector3(i, j, k), Cube);
                    if (i > 0)
                    {
                        fixedJointBase = bricks[new Vector3(i, j, k)].AddComponent<FixedJoint>();
                        fixedJointBase.connectedBody = bricks[new Vector3(i - 1, j, k)].GetComponent<Rigidbody>();
                        fixedJointBase.breakForce = 800; //Esto en 10000 crea filas

                    }
                    if (j > 0)
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
                    Cube.AddComponent<Brick>().Init(new Vector3(i, j, k), this);


                }
            }
        } //Bricks generation

    }

    public GameObject[] UltimosLadrillos()
    {
        GameObject[] lista = new GameObject[(int)(area.y*area.z)];
        for (int k = 0; k < area.z; k++) // Z = Profundo
        {
            for (int j = 0; j < area.y; j++) // Y = Altura
            {
                lista[j] = bricks[new Vector3(area.x-1, j, k)];
            }
        }
        return lista;
    }
    public GameObject[] PrimerosLadrillos()
    {
        GameObject[] lista = new GameObject[(int)(area.y * area.z)];
        for (int k = 0; k < area.z; k++) // Z = Profundo
        {
            for (int j = 0; j < area.y; j++) // Y = Altura
            {
                lista[j] = bricks[new Vector3(0, j, k)];
            }
        }
        return lista;
    }

    public void CheckCollision(Vector3 posicion, Vector3 impulso)
    {
        if (impulso.magnitude > 100)
        {
            PropagateImpact(posicion, impulso/expansionOnda);
        }
    }
     private void PropagateImpact(GameObject ladrillo, Vector3 fuerza)
     {
         Vector3 posicion = ladrillo.GetComponent<Brick>().pos;
         
         _PropagateImpact(posicion, fuerza);

     }
     private void PropagateImpact(Vector3 posicion, Vector3 fuerza)
     {
         
         _PropagateImpact(posicion, fuerza);

     }

    private void _PropagateImpact(Vector3 posicion, Vector3 fuerza)
    {

         if (fuerza.magnitude > 100)
         {
             GameObject ladrilloContinuo;

             if (posicion.x < area.x - 1)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(1, 0, 0)];
                 if (Vector3.Distance(ladrilloContinuo.transform.position,transform.position) < brickSize.x)
                 {
                     ladrilloContinuo.GetComponent<Rigidbody>().AddForce(fuerza,ForceMode.Force);
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / _brickSize.x);
                 }
             }
             if(posicion.x > 0)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(-1, 0, 0)];
                 if (Vector3.Distance(ladrilloContinuo.transform.position, transform.position) < brickSize.x)
                 {
                     ladrilloContinuo.GetComponent<Rigidbody>().AddForce(fuerza, ForceMode.Impulse);
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / _brickSize.x);
                 }
             }

             if (posicion.y < area.y - 1)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(0, 1, 0)];
                 if (Vector3.Distance(ladrilloContinuo.transform.position, transform.position) < brickSize.y + 0.25f)
                 {
                     ladrilloContinuo.GetComponent<Rigidbody>().AddForce(fuerza, ForceMode.Impulse);
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / _brickSize.y);
                 }
             }
             if (posicion.y > 0)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(0, -1, 0)];
                 if (Vector3.Distance(ladrilloContinuo.transform.position, transform.position) < brickSize.y + 0.25f)
                 {
                     ladrilloContinuo.GetComponent<Rigidbody>().AddForce(fuerza, ForceMode.Impulse);
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / _brickSize.y);
                 }
             }

             if (posicion.z < area.z - 1)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(0, 0, 1)];
                 if (Vector3.Distance(ladrilloContinuo.transform.position, transform.position) < brickSize.z + 0.25f)
                 {
                     ladrilloContinuo.GetComponent<Rigidbody>().AddForce(fuerza, ForceMode.Impulse);
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / _brickSize.z);
                 }
             }
             if (posicion.z > 0)
             {
                 ladrilloContinuo = bricks[posicion + new Vector3(0, 0, -1)];
                 if (Vector3.Distance(ladrilloContinuo.transform.position, transform.position) < brickSize.z + 0.25f)
                 {
                     ladrilloContinuo.GetComponent<Rigidbody>().AddForce(fuerza, ForceMode.Impulse);
                     PropagateImpact(ladrilloContinuo, fuerza / expansionOnda / _brickSize.z);
                 }
             }




         }
     }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(PrimerosLadrillos()[0].transform.position, 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(UltimosLadrillos()[0].transform.position, 0.5f);
    }

}

