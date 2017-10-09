//======================================
/*
	@autor  ktk.kumamoto
	@date   2015.3.6 create
	@note   UvScroll
*/
//======================================

using UnityEngine;
using System.Collections;

public class UvScroll : MonoBehaviour {

	public float x_scrollSpeed = 0.5F;
	public float y_scrollSpeed = 0.5F;
	private Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		float x_offset = Time.time * - x_scrollSpeed;
		float y_offset = Time.time * - y_scrollSpeed;

		rend.material.SetTextureOffset("_MainTex", new Vector2(x_offset, y_offset));
	}
}