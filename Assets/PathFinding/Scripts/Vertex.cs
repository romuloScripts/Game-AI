using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum VertexState{
	start,
	end,
	free,
	blocked
}

public class Vertex : MonoBehaviour {

	public VertexState state= VertexState.free;
	public List<Vertex> neighbors;
	
	void Start(){
		Renderer render = GetComponent<Renderer>();
		switch ((int)state) {
			case 0: render.material.color = Color.green; break;
			case 1: render.material.color = Color.blue; break;
			case 3: render.material.color = Color.red; break;
		}
	}
}
