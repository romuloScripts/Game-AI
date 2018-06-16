using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dijkstra : MonoBehaviour {

	[System.Serializable]
	public class VertexTable{
		public List<Step> steps = new List<Step>();	
		public bool finished;
	}
	
	[System.Serializable]
	public class Step{
		public int? vertex;
		public float cost;
	}
	
	public Graph graph;
	public List<int> bestPath = new List<int>();

	[HideInInspector]
	public VertexTable[] vertexTables;
	private bool[,] adjacencies;
	private float [,] weights;
	
	
	void Start () {
		adjacencies = graph.GetAdjacencies();
		weights = graph.GetWeights();
		vertexTables = new VertexTable[graph.vertex.Count];
		for (int i = 0; i < vertexTables.Length; i++) {
			vertexTables[i] = new VertexTable();
		}
		Search();
	}
	
	
	void LateUpdate(){
		for (int i = 1; i < bestPath.Count; i++) {
			Debug.DrawLine(graph.vertex[bestPath[i-1]].transform.position,
			               graph.vertex[bestPath[i]].transform.position,
			               Color.green);
		}
	}
	
	void Search(){
		int smallPath = graph.GetStart();
		Step p = new Step();
		p.vertex = smallPath;
		p.cost = 0;
		vertexTables[smallPath].steps.Add(p);
		bool finished=false;
		while(!finished){
			float small = float.MaxValue;
			vertexTables[smallPath].finished = true;
			finished = true;
		
			DijstraStep(smallPath);

			for (int i = 0; i < vertexTables.Length; i++){
				if(!vertexTables[i].finished && vertexTables[i].steps[vertexTables[i].steps.Count-1].cost < small 
					&& vertexTables[i].steps[vertexTables[i].steps.Count-1].cost != Mathf.Infinity){
					smallPath = i;
					small = vertexTables[i].steps[vertexTables[i].steps.Count-1].cost;
					finished = false;
				}
			}
		}		
		GenerateBestPath();
	}
	
	void DijstraStep(int smallPath){
		for (int i = 0; i < vertexTables.Length; i++){
			if(!vertexTables[i].finished){
				Step p = new Step();
				if(graph.vertex[i].state == VertexState.blocked){
					p.cost = Mathf.Infinity;
					vertexTables[i].steps.Add(p);	
					vertexTables[i].finished = true;
				}else{
					if(adjacencies[smallPath,i]){
						p.vertex = smallPath;
						p.cost = weights[smallPath,i]+vertexTables[smallPath].steps[vertexTables[smallPath].steps.Count-1].cost;
						if(vertexTables[i].steps.Count >0){
							Step previous = vertexTables[i].steps[vertexTables[i].steps.Count-1];
							if(previous != null && p.cost >= previous.cost){
								p.vertex = previous.vertex;
								p.cost = previous.cost;
							}
						}	
						vertexTables[i].steps.Add(p);	
					}else{
						if(vertexTables[i].steps.Count >0){
							Step previous = vertexTables[i].steps[vertexTables[i].steps.Count-1];
							p.cost = previous.cost;
							p.vertex = previous.vertex;
						}else{
							p.cost = Mathf.Infinity;
						}
						vertexTables[i].steps.Add(p);
					}
				}
			}
		}	
	}
	
	void GenerateBestPath(){
		bestPath.Add(graph.GetEnd());
		while(!bestPath.Contains(graph.GetStart()) ){
			List<Step> steps = vertexTables[bestPath[0]].steps;
			if(steps[steps.Count-1].vertex == null){
				Debug.Log("Its not possible to complete the path");
				break;
			}else
				bestPath.Insert(0,(int)steps[steps.Count-1].vertex);	
		}	
	}
}
