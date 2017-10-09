using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.Experimental.UIElements.StyleEnums;
using System.Runtime.Serialization;

public class PowerUps : MonoBehaviour {
	
	GameObject player;
	MyPlayerManager playerScript;
	AndroidControl androidScript;

	public float tumble;
	public float speed;
	public GameObject explosion;
	public int bonusNumber = 1;
	public AudioSource audioSource;

	bool isDead;


	// On Awake
	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");

		playerScript = player.GetComponent<MyPlayerManager> ();
		androidScript = player.GetComponent<AndroidControl> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void Start(){

		transform.LookAt (player.transform.position);
		GetComponent<Rigidbody> ().velocity += transform.forward * speed;
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;
	}
	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject == player) {
			
			audioSource.Play ();
			switch(bonusNumber){
			case 1:
				// HEALTH BOOST 
				playerScript.PwrUp_Health ();
				
				break;
			case 2:
				/// SPEED BOOST
				playerScript.PwrUp_Speed ();
				androidScript.PwrUp_Speed ();
				break;

			case 3:
				/// DUAL FIRE BOOST
				playerScript.PwrUp_Dual ();
				
				break;
			case 4:
				/// SUPPORT
				playerScript.PwrUp_Support ();
				break;
			case 5:
				/// SHEILD
				playerScript.PwrUp_Shield ();
				break;
			}
			Delete ();
		}
	}


	void OnTriggerExit(Collider other)
	{

		if(other.gameObject == player){

		}
	}


	void Delete()
	{
		Debug.Log ("Should Delete PowerUp");
		GameObject clone;
		clone = Instantiate (explosion, player.transform.position, player.transform.rotation);
		Destroy (gameObject);
		DestroyObject (clone, 2);

	}

}