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

public static class FabricaCross
{

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
            UnirParedes(columna.GetComponent<BigWall>().PrimerosLadrillos(), semipared.GetComponent<BigWall>().PrimerosLadrillos());
        }
        if ((cross.partes & Signos.Sur) > 0)
        {
            count++;
            semipared = CrearCrossI(Signos.Sur);
            semipared.transform.parent = pared.transform;
            semipared.transform.position += cross.postion;
            UnirParedes(columna.GetComponent<BigWall>().PrimerosLadrillos(), semipared.GetComponent<BigWall>().PrimerosLadrillos());
        }
        if ((cross.partes & Signos.Este) > 0)
        {
            count++;
            semipared = CrearCrossI(Signos.Este);
            semipared.transform.parent = pared.transform;
            semipared.transform.position += cross.postion;
            UnirParedes(columna.GetComponent<BigWall>().PrimerosLadrillos(), semipared.GetComponent<BigWall>().PrimerosLadrillos());
        }
        if ((cross.partes & Signos.Oeste) > 0)
        {
            count++;
            semipared = CrearCrossI(Signos.Oeste);
            semipared.transform.parent = pared.transform;
            semipared.transform.position += cross.postion;
            UnirParedes(columna.GetComponent<BigWall>().PrimerosLadrillos(), semipared.GetComponent<BigWall>().PrimerosLadrillos());
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
            pared.transform.rotation = Quaternion.Euler(0, 180, 0);
            pared.name = "Sur";
        }
        else if (signo.Equals(Signos.Oeste))
        {
            pared.transform.position += new Vector3(0, 0, 3);
            pared.transform.rotation = Quaternion.Euler(0, 270, 0);
            pared.name = "Oeste";
        }
        
        return pared;
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
    

}

public class WallBuilder : MonoBehaviour {


    Dictionary<Vector3, GameObject> crosses;
    Vector3 posInicial;

    
    //TODO: 
    //Crear crosses (Is, Ls, Ts y Xs) (Achicar cuadrados para que no queden huecos, intercalar joints)
    //Buscar forma de intercalar Cross's? (Compartir paredes?)

	// Use this for initialization
	void Start () {
        //Size brickSize = new Vector3(1.5f, 0.375f, 0.75f);    //Unidad ocupa 6*3*0.75   
        //Angulos Rectos += new Vector3(7.5,0,2.25) con  angulos de += Quaternion.Euler(0, 90, 0));
        //GameObject pared = new GameObject("Pared");
        //IWall scriptPared = pared.AddComponent<BigWall>();
        posInicial = new Vector3(30, 0, 298);
        crosses = new Dictionary<Vector3, GameObject>();

        GameObject.Destroy(FabricaCross.CrearCross(new Cross(Signos.Norte | Signos.Sur | Signos.Este | Signos.Oeste, Signos.Ninguno, PosicionACoordenada(new Vector3(1, 1, 1)))));//arregla las posiciones :3

        crosses.Add(new Vector3(1, 1, 1), FabricaCross.CrearCross(new Cross(Signos.Norte | Signos.Sur | Signos.Este | Signos.Oeste, Signos.Ninguno, PosicionACoordenada(new Vector3(1, 1, 1)))));
        crosses.Add(new Vector3(2, 1, 1), FabricaCross.CrearCross(new Cross(Signos.Norte | Signos.Sur | Signos.Este | Signos.Oeste, Signos.Ninguno, PosicionACoordenada(new Vector3(2, 1, 1)))));
        crosses.Add(new Vector3(1, 1, 2), FabricaCross.CrearCross(new Cross(Signos.Norte | Signos.Sur | Signos.Este | Signos.Oeste, Signos.Ninguno, PosicionACoordenada(new Vector3(1, 1, 2)))));
        crosses.Add(new Vector3(2, 1, 2), FabricaCross.CrearCross(new Cross(Signos.Norte | Signos.Sur | Signos.Este | Signos.Oeste, Signos.Ninguno, PosicionACoordenada(new Vector3(2, 1, 2)))));

    }

    Vector3 PosicionACoordenada(Vector3 posicion)
    {
        return new Vector3(posicion.x * BigWall.brickSize.z * 15, 0, posicion.z * BigWall.brickSize.z * 15) + posInicial;
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
