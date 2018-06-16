using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GraphInfo{

	public bool[,] adjacencies;
	public float [,] weight;
	public float[] heuristics;
	
	public GraphInfo(List<Vertex> vertices){
		FillMatrices(vertices);
	}
	
	public void FillMatrices(List<Vertex> vertices){
		adjacencies = new bool[vertices.Count,vertices.Count];
		weight = new float[vertices.Count,vertices.Count];
		
		for (int i = 0; i < vertices.Count; i++) {
			for (int j = 0; j < vertices.Count; j++) {
				if(vertices[i].neighbors.Contains(vertices[j])){
					adjacencies[i,j] = true;
					weight[i,j] = Vector3.Distance(vertices[i].transform.position,vertices[j].transform.position);
				}else{
					if(vertices[i] == vertices[j]){
						weight[i,j] = 0;
					}else{
						weight[i,j] = Mathf.Infinity;
					}
				}
			}
		}		
	}
	
	public void FillHeuristics(List<Vertex> vertices){
		heuristics = new float[vertices.Count];
		Vertex destiny=null;
		foreach (var item in vertices) {
			if(item.state == VertexState.end){
				destiny = item;
			}
		}
		if(destiny == null ) return;
		for (int i = 0; i < vertices.Count; i++) {
			heuristics[i] = Vector3.Distance(vertices[i].transform.position,destiny.transform.position);
		}
	}	
}
