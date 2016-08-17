using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ICross
{
    public bool north, south, east, west;
    private int count { public get; }

}

public class FabricaCross
{
    static ICross CrearCross(ICross cross)
    {
        return cross;
    }
}

public class CrossI : ICross
{
    private Quaternion rotation { public get; set; }

}
public class WallBuilder : MonoBehaviour {

    public GameObject wall;
    List<GameObject> listaWalls;


    //TODO: 
    //Crear crosses (Is, Ls, Ts y Xs) (Achicar cuadrados para que no queden huecos, intercalar joints)
    //Hacer que al instanciar Cross count se actualize (constructor?)
    //Crear instanciador de crosses(que use count para el tipo de cross, la posición y los valores NSEO)
    //Buscar forma de intercalar Cross's? (Compartir paredes?)

	// Use this for initialization
	void Start () {
        //Size brickSize = new Vector3(1.5f, 0.375f, 0.75f);    //Unidad ocupa 6*3*0.75   
        //Angulos Rectos += new Vector3(7.5,0,2.25) con  angulos de += Quaternion.Euler(0, 90, 0));
        GameObject pared = new GameObject("Pared");
        IWall scriptPared = pared.AddComponent<BigWall>();

        Instantiate(wall, new Vector3(55, 0, 298), Quaternion.Euler(0, 0, 0));
        Instantiate(wall, new Vector3(62.5f, 0, 300.25f), Quaternion.Euler(0, 90, 0));
        Instantiate(wall, new Vector3(64.75f, 0, 292.75f), Quaternion.Euler(0, 180, 0));
        Instantiate(wall, new Vector3(57.25f, 0, 290.5f), Quaternion.Euler(0, 270, 0));
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
