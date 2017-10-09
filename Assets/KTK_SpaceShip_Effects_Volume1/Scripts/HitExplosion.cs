using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class HitExplosion : MonoBehaviour {

	public GameObject explosion;


	public void Explosion () {
		Instantiate (explosion, transform.position, transform.rotation);
	}
	
}
