using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    Rigidbody rigid;
    Vector3 ExplosionPoint;
    [SerializeField]
    float tiempoDeVida;
	// Use this for initialization
	void Start () 
    {
        tiempoDeVida = 15f;
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       //rigid.AddExplosionForce(2500, ExplosionPoint, 5); 8K
       rigid.AddExplosionForce(50000, ExplosionPoint, 5); // 18K
       tiempoDeVida -= Time.deltaTime;

       if (tiempoDeVida <= 0)
       {
           gameObject.SetActive(false);
       }
	}

    public void SetExplosionPointAndReset(Vector3 punto)
    {
        ExplosionPoint = punto;
        tiempoDeVida = 15;
    }

    
}
