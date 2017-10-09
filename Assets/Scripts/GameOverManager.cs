using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameOverManager : MonoBehaviour {

	string gameId = "1548713";
	GameObject player;
	public float restartDelay = 5f;
	public Animator anim;
	public float restartTimer;
	int adRandom = 0;


	void Start(){
		player = GameObject.FindGameObjectWithTag ("PLayer");
	}

	void Awake(){
		
		anim = GetComponent < Animator> ();
	}

	void Update(){

		// Get Variable from script object of player, check if dead or not.
		if(!GameObject.FindGameObjectWithTag ("Player")){
			
			anim.SetTrigger ("GameOver");
			restartTimer += Time.deltaTime;

//			Debug.Log ("Adrandom is : " + adRandom);

			if(restartTimer >= restartDelay){

				RandomADNumber ();
				//SceneManager.LoadScene ("Level 1");
				//DontDestroyOnLoad (player);
				 Application.LoadLevel(Application.loadedLevel);

			}
		}
	}

	private void RandomADNumber()
	{
		adRandom = Random.Range(0, 2);
		Debug.Log(adRandom);

		if (adRandom == 1) {
			adRandom = 0;
			Advertisement.Show ();
		}

	}
}
