using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
    public Material Mate;
	// Use this for initialization
	void Start () {
        GameObject Cube;
        
        for (int i = 50; i < 66; i +=2)
        {
            for (int j = 0; j < 16; j += 2)
            {
                for (int k = 300; k < 316; k += 2)
                {
                    Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Cube.transform.parent = transform;
                    Cube.transform.position = new Vector3(i, j, k);
                    Cube.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    Cube.AddComponent<Rigidbody>();
                    Cube.GetComponent<MeshRenderer>().materials[0] = Mate;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
