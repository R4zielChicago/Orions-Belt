using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System;

public class PreFabManager : MonoBehaviour {

	public GameObject prefab;
	//Singleton
	private static PreFabManager singleton_Instance = null;

	public static PreFabManager instance{

		get{
			if (singleton_Instance == null) {
				singleton_Instance = (PreFabManager)FindObjectOfType
					(typeof(PreFabManager));
				Debug.Log ("No PreFab object could be found");

			} else{
				Debug.Log ("PreFab object found: " + instance.name);
				
			}

			return singleton_Instance;
		}
	}
}
