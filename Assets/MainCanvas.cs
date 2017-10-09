using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour {

	public Animator anim;

	void Awake () {
		anim = GetComponent < Animator> ();
		if(!GameObject.Find ("Player")){

			anim.SetTrigger ("GameOver");
		}
	}

	
	// Update is called once per frame
	void Update () {

	}
}
