using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour 
{
	[SerializeField] private GameObject pausePanel;

	private bool paused;
	void Start()
	{
		paused = false;
		//pausePanel.SetActive(false);
	}
	void Update()
	{
		

//		if(Input.GetKeyDown (KeyCode.Escape)) 
//		{
//			if (!pausePanel.activeInHierarchy) 
//			{
//				PauseGame();
//			}
//			if (pausePanel.activeInHierarchy) 
//			{
//				ContinueGame();   
//			}
//		} 
	}
	 public void PauseGame()
	{
				
		Time.timeScale = 0;
		paused = true;
		pausePanel.SetActive(true);
		//Disable scripts that still work while timescale is set to 0
	} 
	 public void ContinueGame()
	{
		Time.timeScale = 1;
		paused = false;
		pausePanel.SetActive(false);
		//enable the scripts again
	}
}
