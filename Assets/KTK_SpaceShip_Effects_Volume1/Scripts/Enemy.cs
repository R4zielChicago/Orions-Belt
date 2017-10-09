using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject explosion;

	public GameObject Eff_Barrier;

	public int enemySpeed = 2;

	void Start () {
		Eff_Barrier = GameObject.Find("Eff_Barrier_00");
		GetComponent<Rigidbody>().velocity = transform.forward.normalized * -enemySpeed;
	}

	void OnTriggerEnter(Collider c){

		if(c.tag=="Bullet"){
			Destroy(c.gameObject);
		}


		if(c.tag != "Enemy"){
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
