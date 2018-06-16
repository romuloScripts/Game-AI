using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreadthFirstSearch : MonoBehaviour {

	public SearchVertex origin;
	public List<SearchVertex> next;
	public List<SearchVertex> current;
	
	void Start(){
		current.Add(origin);
		next.Add(origin);
		StartCoroutine(BFS());
	}
	
	IEnumerator BFS(){
		while(next.Count >0){
			yield return new WaitForSeconds(2);
			while(current.Count >0){
				Expandir(current[0]);
				current.RemoveAt(0);	
			}
			current.AddRange(next);
		}
	}
	
	void Expandir(SearchVertex vertice){	
		next.Remove(vertice);
		TacarFogo(vertice.down);
		TacarFogo(vertice.left);
		TacarFogo(vertice.right);
		TacarFogo(vertice.up);		
	}
	
	void TacarFogo(SearchVertex vertice){
		if(vertice && vertice.areaState == AreaState.normal){
			vertice.GetComponent<Renderer>().material.color = Color.red;
			vertice.areaState = AreaState.fire;
			next.Add(vertice);
		}
	}
}
