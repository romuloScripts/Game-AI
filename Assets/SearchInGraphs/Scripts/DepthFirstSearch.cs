using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DepthFirstSearch : MonoBehaviour {

	public SearchVertex origin;

	void Start () {	
		StartCoroutine(DFS(origin));
	}
	
	IEnumerator DFS(SearchVertex vertex){	
		
		yield return new WaitForSeconds(0.5f);
		vertex.GetComponent<Renderer>().material.color = Color.red;
		vertex.areaState = AreaState.fire;
		if(Check(vertex.down)){
			yield return StartCoroutine(DFS(vertex.down));
		}
		if(Check(vertex.left)){
			yield return StartCoroutine(DFS(vertex.left));
		}
		if(Check(vertex.up)){
			yield return StartCoroutine(DFS(vertex.up));
		}
		if(Check(vertex.right)){
			yield return StartCoroutine(DFS(vertex.right));
		}
	}
	
	bool Check(SearchVertex vertice){
		if(vertice && vertice.areaState == AreaState.normal){
			return true;
		}else{
			return false;
		}
	}
}