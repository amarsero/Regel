using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    Rigidbody rigid;
    Vector3 ExplosionPoint;
    float tiempoDeVida;
	// Use this for initialization
	void Start () 
    {
        tiempoDeVida = 15;
        rigid = GetComponent<Rigidbody>();
        ExplosionPoint = GameObject.FindGameObjectWithTag("Explosion").transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	   rigid.AddExplosionForce(1200, ExplosionPoint, 5);

       tiempoDeVida -= Time.deltaTime;

       if (tiempoDeVida <= 0) Destroy(gameObject);
	}

  
    
}
