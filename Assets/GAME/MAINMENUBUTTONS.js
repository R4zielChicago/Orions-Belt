#pragma strict

import  UnityEngine.SceneManagement;
function StartGame(){

//Application.LoadLevel(1);
//SceneManager.LoadScene("MainMenu");
	Application.LoadLevel(Application.loadedLevel);


}

function LevelSelect(){
	SceneManager.LoadScene("LevelSelect");

	//Application.LoadLevel("LevelSelect");


	

}

function QuitGame(){
Application.Quit();
//SceneManager.Quit();

}