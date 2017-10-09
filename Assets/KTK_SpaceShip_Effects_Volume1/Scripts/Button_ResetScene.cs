//======================================
/*
@autor ktk.kumamoto
@date 2015.4.21 create
@note Button_ResetScene
*/
//======================================

using UnityEngine;
using System.Collections;

public class Button_ResetScene : MonoBehaviour {

	
	void OnGUI()
	{
		if(GUI.Button(new Rect(500, 10, 100, 30), "ResetScene")){
			Application.LoadLevel("KTK_SpaceShip_Demo");
		}
	}
}