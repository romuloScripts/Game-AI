using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph : MonoBehaviour {

	public List<Vertex> vertex;
	public GraphInfo graphInfo;

	private int ini, end;
	
	void Awake() {
		graphInfo = new GraphInfo(vertex);
		Verify();
	}
	
	void Verify(){
		bool findedIni=false, findedEnd=false;

		for (int i = 0; i < vertex.Count; i++) {
			if(vertex[i].state == VertexState.end){
				end = i;
				findedEnd = true;
			}else if(vertex[i].state == VertexState.start){
				ini = i;
				findedIni= true;
			}
			if(findedEnd && findedIni)
				i = vertex.Count;		
		}
		if(!findedEnd){
			vertex[vertex.Count-1].state = VertexState.end;
		}
		if(!findedIni){
			vertex[0].state = VertexState.start;
		}
	}
	
	void Update(){
		for (int i = 0; i < graphInfo.adjacencies.GetLength(0); i++) {
			for (int j = 0; j < graphInfo.adjacencies.GetLength(1); j++) {
				if(graphInfo.adjacencies[i,j])
					Debug.DrawLine(vertex[i].transform.position+(Vector3.forward*0.1f),vertex[j].transform.position+(Vector3.forward*0.1f),Color.blue);
			}
		}
	}
	
	public void FillHeuristics(){
		graphInfo.FillHeuristics(vertex);
	}
	
	public bool[,] GetAdjacencies(){
		return graphInfo.adjacencies;
	}
	
	public float [,] GetWeights(){
		return graphInfo.weight;
	}
	
	public float [] GetHeuristic(){
		if(graphInfo.heuristics == null)
			FillHeuristics();
		return graphInfo.heuristics;
	}
	
	public int GetStart(){
		return ini;
	}
	
	public int GetEnd(){
		return end;
	}
}
