using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Flags] 
public enum Signos : byte
{
    Ninguno = 0x00,
    Norte = 0x01,
    Sur = 0x02,
    Este = 0x04,
    Oeste = 0x08
}

public class Cross
{
    public Cross()
    {

    }
    public Cross(Signos partes, Signos bordes, Vector3 posicion)
    {
        this.partes = partes;
        this.bordes = bordes;
        this.postion = posicion;
    }
    public Signos partes { get; protected set; }
    public Signos bordes { get; protected set; }
    public Vector3 postion { get; protected set; }


}

public class FabricaCross
{
    //public static GameObject CrearCross(Cross cross)
    //{
    //    int count = 0;
    //    if ((int)(cross.partes & Signos.Norte) > 0) count++;
    //    if ((int)(cross.partes & Signos.Sur) > 0) count++;
    //    if ((int)(cross.partes & Signos.Este) > 0) count++;
    //    if ((int)(cross.partes & Signos.Oeste) > 0) count++;

    //    if (count == 4) return CrearCrossX(cross);
    //    else if (count == 3) return CrearCrossT(cross);
    //    else if (count == 2)
    //    {
    //        if (cross.partes.Equals(Signos.Norte | Signos.Sur) || cross.partes.Equals(Signos.Este | Signos.Oeste))
    //        {
    //            return CrearCrossI(cross,true);
    //        }
    //        else return CrearCrossL(cross);
    //    }
    //    else if (count ==1) return CrearCrossI(cross, false); 

    //    throw new System.Exception("FabricaCross: Count es 0"); //Borrar linea in deploy

    //}

    public static GameObject CrearCross(Cross cross)
    {

        int count = 0;
        GameObject pared = new GameObject("I: " + cross.postion.ToString());

        GameObject columna = new GameObject("Columna");
        columna.AddComponent<BigWall>().CrearColumna();
        columna.transform.parent = pared.transform;
        columna.transform.position += cross.postion;

        GameObject semipared;

        if ((cross.partes & Signos.Norte) > 0)
        {
            count++;
            semipared = CrearCrossI(Signos.Norte);
            semipared.transform.parent = pared.transform;          
            semipared.transform.position += cross.postion;        
        }
        if ((cross.partes & Signos.Sur) > 0)
        {
            count++;
            semipared = CrearCrossI(Signos.Sur);
            semipared.transform.parent = pared.transform;
            semipared.transform.position += cross.postion;
        }
        if ((cross.partes & Signos.Este) > 0)
        {
            count++;
            semipared = CrearCrossI(Signos.Este);
            semipared.transform.parent = pared.transform;
            semipared.transform.position += cross.postion;
        }
        if ((cross.partes & Signos.Oeste) > 0)
        {
            count++;
            semipared = CrearCrossI(Signos.Oeste);
            semipared.transform.parent = pared.transform;
            semipared.transform.position += cross.postion;
        }

        if (count == 4) pared.name = "X: " + cross.postion.ToString();
        else if (count == 3) pared.name = "T: " + cross.postion.ToString();
        else if (count == 2)
        {
            if (cross.partes.Equals(Signos.Norte | Signos.Sur) || cross.partes.Equals(Signos.Este | Signos.Oeste))
            {
                pared.name = "I2: " + cross.postion.ToString();
            }
            else pared.name = "L: " + cross.postion.ToString();
        }
        else pared.name = "I: " + cross.postion.ToString();

        return pared;

    }


    /// <summary>
    /// Crea un cross en forma de I
    /// </summary>
    /// <param name="cross">Cross a crear</param>
    /// <param name="doble">Si la longitud del cross es doble</param>
    /// <returns></returns>
    private static GameObject CrearCrossI(Cross cross,bool doble)
    {

       
        
        GameObject pared = new GameObject("I: " + cross.postion.ToString());

        GameObject columna = new GameObject("Columna");
        columna.AddComponent<BigWall>().CrearColumna();
        columna.transform.parent = pared.transform;
        columna.transform.position += cross.postion;

        GameObject semipared;


        if(cross.partes.Equals(Signos.Este | Signos.Oeste))
        {
            semipared = CrearCrossI(Signos.Este);            
            semipared.transform.position += cross.postion;
            semipared.transform.parent = pared.transform;        
        
            semipared = CrearCrossI(Signos.Oeste);            
            semipared.transform.position += cross.postion;
            semipared.transform.parent = pared.transform;             
        }
        else if (cross.partes.Equals(Signos.Norte | Signos.Sur))
        {
            semipared = CrearCrossI(Signos.Norte);
            semipared.transform.position += cross.postion;
            semipared.transform.parent = pared.transform;

            semipared = CrearCrossI(Signos.Sur);
            semipared.transform.position += cross.postion;
            semipared.transform.parent = pared.transform;      
        }
        else
        {
            semipared = CrearCrossI(cross.partes);
            semipared.transform.parent = pared.transform;          
            semipared.transform.position += cross.postion;
        }

        return pared;
    }

    private static GameObject CrearCrossI(Signos signo)
    {

        GameObject pared = new GameObject(signo.ToString());
        pared.AddComponent<BigWall>().CrearPared(false);

        if (signo.Equals(Signos.Norte))
        {
            pared.transform.position += new Vector3(3, 0, 0);
            pared.name = "Norte";
        }
        else if (signo.Equals(Signos.Este))
        {
            pared.transform.position += new Vector3(0, 0, -3);
            pared.transform.rotation = Quaternion.Euler(0, 90, 0);
            pared.name = "Este";
        }
        else if (signo.Equals(Signos.Sur))
        {
            pared.transform.position += new Vector3(-3, 0, 0);
            pared.name = "Sur";
        }
        else if (signo.Equals(Signos.Oeste))
        {
            pared.transform.position += new Vector3(0, 0, 3);
            pared.transform.rotation = Quaternion.Euler(0, 90, 0);
            pared.name = "Oeste";
        }
        
        return pared;
    }
    private static GameObject CrearCrossL(Cross cross)
    {
        GameObject semipared1;
        GameObject semipared2;
        GameObject pared = new GameObject("L: " + cross.postion.ToString());
        pared.transform.position = cross.postion;

        if (cross.partes.Equals(Signos.Norte | Signos.Este))
        {
            semipared1 = CrearCrossI(Signos.Norte);
            semipared2 = CrearCrossI(Signos.Este);


        }
        else if (cross.partes.Equals(Signos.Este | Signos.Sur))
        {
            semipared1 = CrearCrossI(Signos.Este);
            semipared2 = CrearCrossI(Signos.Sur);
            ArreglarTerminaciones(Signos.Sur, semipared2.GetComponent<BigWall>().UltimosLadrillos());
        }
        else if (cross.partes.Equals(Signos.Oeste | Signos.Sur))
        {
            semipared1 = CrearCrossI(Signos.Sur);
            semipared2 = CrearCrossI(Signos.Oeste);

            ArreglarTerminaciones(Signos.Sur, semipared1.GetComponent<BigWall>().UltimosLadrillos());
        }
        else 
        {
            semipared1 = CrearCrossI(Signos.Norte);
            semipared2 = CrearCrossI(Signos.Oeste);
        }

        semipared1.transform.parent = pared.transform;
        semipared1.transform.position += pared.transform.position;
        semipared2.transform.parent = pared.transform;
        semipared2.transform.position += pared.transform.position;

        //if (cross.partes.Equals(Signos.Este | Signos.Norte))
        //{
        //    UnirParedes(semipared1.GetComponent<BigWall>().PrimerosLadrillos(), semipared2.GetComponent<BigWall>().PrimerosLadrillos());
        //}
        //else if (cross.partes.Equals(Signos.Oeste | Signos.Sur))
        //{
        //    UnirParedes(semipared1.GetComponent<BigWall>().UltimosLadrillos(), semipared2.GetComponent<BigWall>().UltimosLadrillos());
        //}
        //else
        //{
        //    UnirParedes(semipared1.GetComponent<BigWall>().PrimerosLadrillos(), semipared2.GetComponent<BigWall>().UltimosLadrillos());
        //}


        return pared;
    }

    private static void ArreglarTerminaciones(Signos signo, GameObject[] lista)
    {
        if (signo.Equals(Signos.Este))
        {


            for (int j = 0; j < lista.Length; j++)
            {
                if ((j % 2) == 0)
                {
                    lista[j].transform.localPosition += new Vector3(BigWall.brickSize.y, 0, 0);
                    lista[j].transform.localScale += new Vector3(-BigWall.brickSize.x / 2, 0, 0);
                }
                else
                {
                    lista[j].transform.localPosition += new Vector3(-BigWall.brickSize.y, 0, 0);
                    lista[j].transform.localScale += new Vector3(BigWall.brickSize.x / 2, 0, 0);
                }
            }
        }
        else if (signo.Equals(Signos.Sur))
        {
            for (int j = 0; j < lista.Length; j++)
            {
                if ((j % 2) == 0)
                {
                    lista[j].transform.localPosition += new Vector3(BigWall.brickSize.y, 0, 0);
                    lista[j].transform.localScale += new Vector3(BigWall.brickSize.x / 2, 0, 0);
                }
                else
                {
                    lista[j].transform.localScale += new Vector3(-BigWall.brickSize.x / 2, 0, 0);
                }
            }
        }

    }

    private static void UnirParedes(GameObject[] lista1, GameObject[] lista2)
    {
        FixedJoint fixedJointBase;
        for (int i = 0; i < lista1.Length; i++)
        {
            fixedJointBase = lista1[i].AddComponent<FixedJoint>();
            fixedJointBase.connectedBody = lista2[i].GetComponent<Rigidbody>();
            fixedJointBase.breakForce = 250;
        }
    }

    private static void RecortarPared(GameObject[] lista1)
    {
        for (int i = 0; i < lista1.Length; i++)
        {
            if ((i % 2) == 1)
            {
                lista1[i].transform.localScale -= new Vector3(BigWall.brickSize.z,0);
                lista1[i].transform.position += new Vector3(BigWall.brickSize.x/50, 0);

            }
        }
    }
    
    private static GameObject CrearCrossT(Cross cross)
    {
        GameObject semipared1;
        GameObject semipared2;
        GameObject semipared3;

        GameObject pared = new GameObject("T: " + cross.postion.ToString());
        pared.transform.position = cross.postion;
        if (cross.partes.Equals(Signos.Norte | Signos.Este | Signos.Sur))
        {
            semipared1 = CrearCrossI(Signos.Norte);
            semipared2 = CrearCrossI(Signos.Este);
            semipared3 = CrearCrossI(Signos.Sur);
            RecortarPared(semipared2.GetComponent<BigWall>().PrimerosLadrillos());

        }
        else
        {
            throw new Exception();
        }

        semipared1.transform.parent = pared.transform;
        semipared1.transform.position += pared.transform.position;
        semipared2.transform.parent = pared.transform;
        semipared2.transform.position += pared.transform.position;
        semipared3.transform.parent = pared.transform;
        semipared3.transform.position += pared.transform.position;

        return pared;

    }
    private static GameObject CrearCrossX(Cross cross)
    {
        throw new System.NotImplementedException();
    }
}

//public class CrossI : Cross
//{
//    public static implicit operator CrossI(Cross cross)
//    {
//        return new CrossI(cross.bordes, cross.partes, cross.postion);
//    }

//    public CrossI(Signos bordes, Signos partes, Vector3 posicion)
//    {
//        this.bordes = bordes;
//        this.partes = partes;
//        this.postion = postion;
//    }
//    public CrossI()
//    {

//    }


//    public Quaternion rotation { get; private set; }


//}

//public class CrossL : Cross
//{
//    public Quaternion rotation { get; private set; }


//}


//public class CrossT : Cross
//{
//    public Quaternion rotation { get; private set; }


//}   

//public class CrossX : Cross
//{


//}
public class WallBuilder : MonoBehaviour {

    List<GameObject> listaWalls; //index[0] = Columna;

    
    //TODO: 
    //Crear crosses (Is, Ls, Ts y Xs) (Achicar cuadrados para que no queden huecos, intercalar joints)
    //Buscar forma de intercalar Cross's? (Compartir paredes?)

	// Use this for initialization
	void Start () {
        //Size brickSize = new Vector3(1.5f, 0.375f, 0.75f);    //Unidad ocupa 6*3*0.75   
        //Angulos Rectos += new Vector3(7.5,0,2.25) con  angulos de += Quaternion.Euler(0, 90, 0));
        //GameObject pared = new GameObject("Pared");
    //    IWall scriptPared = pared.AddComponent<BigWall>();

        FabricaCross.CrearCross(new Cross(Signos.Norte, Signos.Ninguno, new Vector3(43, 0, 310))); //.transform.parent = transform


    }


	
	// Update is called once per frame
	void Update () {
	
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(55, 0, 298), new Vector3(55, 10, 298));
    }
}
