using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour {

	public Graph graph;

	[System.Serializable]
	public class Path{
		public List<int> vertices = new List<int>();
		public float cost;
		public float heuristicCost;
	}
	
	public Path bestPath;
	[HideInInspector]
	public List<Path> paths;
	private bool[,] adjacencies;
	private float [,] weights;
	private float[] heuristics;
	

	public void Start(){
		adjacencies = graph.GetAdjacencies();
		weights = graph.GetWeights();
		heuristics = graph.GetHeuristic();
		Search();
	}
	
	void LateUpdate(){
		if(bestPath == null) return; 
		for (int i = 1; i < bestPath.vertices.Count; i++) {
			Debug.DrawLine(graph.vertex[bestPath.vertices[i-1]].transform.position,
			               graph.vertex[bestPath.vertices[i]].transform.position,
			               Color.green);
		}
	}
	
	void Search(){
		int i = graph.GetStart();
		Path c = new Path();
		c.vertices.Add(i);
		paths.Add(c);
		Expand(c);
		int smallPath=0;
		do{
			if(paths != null && paths.Count >0 ){
				Expand(paths[smallPath]);
				float small = float.MaxValue; 
				if(paths != null && paths.Count >0 ){
					for (int j = 0; j < paths.Count; j++) {
						if(paths[j].heuristicCost < small || (paths[j].heuristicCost == small && paths[j].vertices.Contains(graph.GetEnd()))){
							small = paths[j].heuristicCost;
							smallPath = j;
						}	
					}
				}
			}
		}while(!Finished(smallPath));
		if(paths.Count >0)
			bestPath = paths[smallPath];
		else{
			Debug.Log("Its not possible to complete the path");
		}
	}
	
	void Expand(Path path){
		paths.Remove(path);
		int i = path.vertices[path.vertices.Count-1];
		for (int j = 0; j < adjacencies.GetLength(0); j++){
			if(adjacencies[i,j] && !path.vertices.Contains(j) && graph.vertex[j].state != VertexState.blocked){
				Path c = new Path();
				c.vertices.AddRange(path.vertices);
				c.vertices.Add(j);
				c.cost = path.cost;
				c.cost += weights[i,j];
				c.heuristicCost = c.cost + heuristics[j];
				paths.Add(c);	
			}
		}
	}
	
	bool Finished(int smallPath){
		if(paths.Count >0){
			if(paths[smallPath].vertices.Contains(graph.GetEnd())){
				return true;
			}
			return false;
		}else{
			return true;
		}
	}
}
