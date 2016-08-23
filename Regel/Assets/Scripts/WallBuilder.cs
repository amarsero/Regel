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
    public static GameObject CrearCross(Cross cross)
    {
        int count = 0;
        if ((int)(cross.partes & Signos.Norte) > 0) count++;
        if ((int)(cross.partes & Signos.Sur) > 0) count++;
        if ((int)(cross.partes & Signos.Este) > 0) count++;
        if ((int)(cross.partes & Signos.Oeste) > 0) count++;

        if (count == 4) return CrearCrossX(cross);
        else if (count == 3) return CrearCrossT(cross);
        else if (count == 2)
        {
            if (cross.partes.Equals(Signos.Norte | Signos.Sur) || cross.partes.Equals(Signos.Este | Signos.Oeste))
            {
                return CrearCrossI(cross,true);
            }
            else return CrearCrossL(cross);
        }
        else if (count ==1) return CrearCrossI(cross, false); 

        throw new System.Exception("FabricaCross: Count es 0"); //Borrar linea in deploy

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
        pared.transform.position = cross.postion;

        pared.AddComponent<BigWall>().CrearPared(doble,false);

        if (doble && cross.partes.Equals(Signos.Este | Signos.Oeste)) pared.transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (cross.partes.Equals(Signos.Norte)) pared.transform.position += new Vector3(3, 0, 0);
        else if (cross.partes.Equals(Signos.Este))
        {
            pared.transform.position += new Vector3(0, 0, 3);
            pared.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (cross.partes.Equals(Signos.Sur))
        {
            pared.transform.position += new Vector3(-3, 0,0 );
        }
        else if (cross.partes.Equals(Signos.Oeste))
        {
            pared.transform.position += new Vector3(0, 0, -3);
            pared.transform.rotation = Quaternion.Euler(0, 90, 0);
        }






        return pared;
    }

    private static GameObject CrearCrossI(Signos signo, bool desfasaje)
    {

        GameObject pared = new GameObject(signo.ToString());
        pared.AddComponent<BigWall>().CrearPared(false, desfasaje);

        if (signo.Equals(Signos.Norte)) pared.transform.position += new Vector3(3, 0, 0);
        else if (signo.Equals(Signos.Este))
        {
            pared.transform.position += new Vector3(0, 0, 3);
            pared.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (signo.Equals(Signos.Sur))
        {
            pared.transform.position += new Vector3(-3, 0, 0);
        }
        else if (signo.Equals(Signos.Oeste))
        {
            pared.transform.position += new Vector3(0, 0, -3);
            pared.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        
        return pared;
    }
    private static GameObject CrearCrossL(Cross cross)
    {
        // Orientación de los Crosses http://i.imgur.com/acUozPG.png
        // Arreglar para que las puntas conecten cuando se rotan
        // Arreglar Este es en realidad Oeste (invertirlos)
        GameObject pared = new GameObject("L: " + cross.postion.ToString());
        pared.transform.position = cross.postion;
        GameObject semipared1 = CrearCrossI(Signos.Norte, false);
        semipared1.transform.parent = pared.transform;
        semipared1.transform.position += pared.transform.position;
        GameObject semipared2 = CrearCrossI(Signos.Este, true);
        semipared2.transform.parent = pared.transform;
        semipared2.transform.position += pared.transform.position;

        UnirParedes(semipared1.GetComponent<BigWall>().PrimerosLadrillos(), semipared2.GetComponent<BigWall>().UltimosLadrillos());

        return pared;
    }

    private static void UnirParedes(GameObject[] lista1, GameObject[] lista2)
    {
        FixedJoint fixedJointBase;
        for (int i = 0; i < lista1.Length; i++)
        {
            fixedJointBase = lista1[i].AddComponent<FixedJoint>();
            fixedJointBase.connectedBody = lista2[i].GetComponent<Rigidbody>();
            fixedJointBase.breakForce = 150;
        }
    }

    private static GameObject CrearCrossT(Cross cross)
    {
        throw new System.NotImplementedException();
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

    public GameObject wall;
    List<GameObject> listaWalls;


    //TODO: 
    //Crear crosses (Is, Ls, Ts y Xs) (Achicar cuadrados para que no queden huecos, intercalar joints)
    //Buscar forma de intercalar Cross's? (Compartir paredes?)

	// Use this for initialization
	void Start () {
        //Size brickSize = new Vector3(1.5f, 0.375f, 0.75f);    //Unidad ocupa 6*3*0.75   
        //Angulos Rectos += new Vector3(7.5,0,2.25) con  angulos de += Quaternion.Euler(0, 90, 0));
        //GameObject pared = new GameObject("Pared");
    //    IWall scriptPared = pared.AddComponent<BigWall>();

    //    Instantiate(wall, new Vector3(55, 0, 298), Quaternion.Euler(0, 0, 0));
    //    Instantiate(wall, new Vector3(62.5f, 0, 300.25f), Quaternion.Euler(0, 90, 0));
    //    Instantiate(wall, new Vector3(64.75f, 0, 292.75f), Quaternion.Euler(0, 180, 0));
    //    Instantiate(wall, new Vector3(57.25f, 0, 290.5f), Quaternion.Euler(0, 270, 0));
    //    FabricaCross.CrearCross(new Cross(Signos.Norte, Signos.Ninguno, new Vector3(55, 0, 298))); //.transform.parent = transform

        FabricaCross.CrearCross(new Cross(Signos.Norte | Signos.Este, Signos.Ninguno, new Vector3(55, 0, 298))); //.transform.parent = transform

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
