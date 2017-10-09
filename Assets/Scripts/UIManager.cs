using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class UIManager : MonoBehaviour {

	GameObject[] pauseObjects;
	int adRandom = 0;

	GameObject player;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		player = GameObject.FindGameObjectWithTag ("Player");
		hidePaused();
	}

	// Update is called once per frame
	void Update () {

		//uses the p button to pause and unpause the game
		if(Input.GetKeyDown(KeyCode.P))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Debug.Log ("high");
				Time.timeScale = 1;
				hidePaused();
			}
		}
	}


	//Reloads the Level
	public void Reload(){

		RandomADNumber ();

		 Application.LoadLevel(Application.loadedLevel);
	}

	//controls the pausing of the scene
	public void pauseControl(){
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPaused();
		} else if (Time.timeScale == 0){
			Time.timeScale = 1;
			hidePaused();
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		Debug.Log ("Inside ShowPaused");
		foreach(GameObject g in pauseObjects){
			Debug.Log ("Should Be Showing" + g);
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		Debug.Log ("Inside hidePaused");
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	//loads inputted level
	public void LoadLevel(string level){

		//Instantiate (player);
		//Application.LoadLevel(level);
		Time.timeScale = 1;
		Advertisement.Show ();
		SceneManager.LoadScene ("MainMenu");


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
