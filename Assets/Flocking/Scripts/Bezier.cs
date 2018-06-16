using UnityEngine;
using System.Collections;
using System;

public class Bezier : Path{
	public Transform p1;
	public Transform p2;
	public Transform p3;
	public Transform p4;
	public float step=0.01f;
	public bool drawGizmos=true;
	
	public override Transform GetFirst(){
		return p1;
	}
	
	public Vector3 calcBezier(float u){
		return calcBezier(p1.position,p2.position,p3.position,p4.position, u);
	}
	
	public static Vector3 calcBezier (Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float u) {
		float n = 1-u;
		return p1*n*n*n + p2*3*u*n*n + p3*3*u*u*n + p4*u*u*u;
	}
	
	GameObject CreateTransform(string nome){
		GameObject g = new GameObject(nome);
		g.transform.parent = transform;
		g.transform.localPosition = Vector3.zero;
		return g;
	}
	
	void Verify(){
		if(p1 == null){
			p1 = CreateTransform("p1").transform;
			p1.position+=Vector3.left*5;
		}
		if(p2 == null){
			p2 = CreateTransform("p2").transform;
			p2.position+=Vector3.left*5;
			p2.position+=Vector3.up*7;
		}
		if(p3 == null){
			p3 = CreateTransform("p3").transform;
			p3.position+=Vector3.right*5;
			p3.position+=Vector3.up*7;
		}
		if(p4 == null){
			p4 = CreateTransform("p4").transform;
			p4.position+=Vector3.right*5;
		}
	}
	
	
	void OnDrawGizmosSelected(){
		if(p2 && p1 && p3 && p4){
			Gizmos.DrawLine(p2.position,p1.position);
			Gizmos.DrawLine(p3.position,p4.position);
		}
	}
	
	void OnDrawGizmos(){												
		Verify();
		if(!drawGizmos) return;
		float u=0;
		float i=0;
		Vector3 vPrevious;
		Vector3 v = calcBezier(p1.position,p2.position,p3.position,p4.position, u);
		while(u != 1){
			vPrevious = v;	
			u = Mathf.Lerp(u,1,i);
			v = calcBezier(p1.position,p2.position,p3.position,p4.position, u);
			if (u != 0.0f) {
				Gizmos.color = Color.green;
				Gizmos.DrawLine(v, vPrevious);
			}
			i += step;
		}
	}
	
	[ContextMenu("Circle")]
	void Circle(){
		GameObject g = new GameObject();
		g.transform.position = transform.position;
		g.AddComponent<Bezier>();
		Bezier b = g.GetComponent<Bezier>();
		b.Verify();
		dir = b;
		OnValidate();
		b.dir = this;
		b.OnValidate();
		g.transform.Rotate(Vector3.forward*180);
		g.transform.parent = transform;
	}
}


