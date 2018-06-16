using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

	public Path dir;
	public Path esq;
	
	
	public virtual Transform GetFirst(){
		return transform;
	}
	
	public virtual void OnValidate(){
		if(dir){
			dir.esq = this;
			dir.OnConectPoints();	
		}
		if(esq){
			esq.dir = this;
			esq.OnConectPoints();
		}
	}
	
	public virtual void OnConectPoints()
	{
		
	}
	
	public virtual Vector3 calcPos(float i)
	{
		return transform.position;
	}
}
