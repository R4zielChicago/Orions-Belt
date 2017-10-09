using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		//Debug.Log ("Destroy Asteroid By Wall");
		Destroy(other.gameObject);
	}
}
