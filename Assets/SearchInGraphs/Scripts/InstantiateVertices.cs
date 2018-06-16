using UnityEngine;
using System.Collections;

public class InstantiateVertices : MonoBehaviour {

	public SearchVertex vertice;
	public int size=4;
	
	private SearchVertex[,] vertices;
	public Vector2 sumXY;
	
	[ContextMenu("MakeVertices")]
	public void MakeVertices(){
		vertices = new SearchVertex[size,size];
		for (int i = 0; i < size; i++){
			for (int j = 0; j < size; j++){
				Vector3 pos = transform.position + new Vector3(sumXY.x*i,sumXY.y*j);
				vertices[i,j] = Instantiate(vertice,pos,transform.rotation) as SearchVertex;
				vertices[i,j].transform.parent = transform;
			}
		}
		
		for (int i = 0; i < size; i++){
			for (int j = 0; j < size; j++){
				if(i+1 <size){
					vertices[i,j].right = vertices[i+1,j];
				}
				if(i-1 >=0){
					vertices[i,j].left = vertices[i-1,j];
				}
				if(j+1 < size){
					vertices[i,j].down = vertices[i,j+1];
				}
				if(j-1 >=0){
					vertices[i,j].up = vertices[i,j-1];
				}
			}
		}
	}
}
