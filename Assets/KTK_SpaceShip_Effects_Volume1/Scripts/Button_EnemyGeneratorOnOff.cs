//======================================
/*
@autor ktk.kumamoto
@date 2015.4.20 create
@note Button_EnemyGeneratorOnOff
*/
//======================================


using UnityEngine;
using System.Collections;


public class Button_EnemyGeneratorOnOff : MonoBehaviour {
	
	
	private bool isChecked = true;
	private string Enemy_State = "Enemy:On";
	
	
	public GameObject EnemyGenerator;
	
	
	void OnGUI()
	{
		Rect rect1 = new Rect(10, 60, 400, 30);
		isChecked = GUI.Toggle(rect1, isChecked, Enemy_State);
		if(EnemyGenerator != null){
			if (isChecked ) {
				Enemy_State = "Enemy:On";
				EnemyGenerator.active = true;
			} else {
				Enemy_State = "Enemy:Off";
				EnemyGenerator.active = false;
			}
		}
	}
}