using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

	public float radius;
	public Bezier bezier;
	public float vel=1;
	private float u;
	[Range(0,1)]
	public float uIni=0;
	[Range(0,1)]
	public float uTarget=0;
	public float magnetDelay=3;

	void Start () {
		u = uIni;
		transform.position = bezier.calcBezier(u);
		Collider[] col = Physics.OverlapSphere(transform.position,radius*1000);
		foreach (var item in col){
			item.transform.LookAt(transform);
		}	
	}

	void Update () {
		Collider[] col = Physics.OverlapSphere(transform.position,radius);
		foreach (var item in col){
			item.transform.LookAt(transform);
			float delay = Mathf.Lerp(2,magnetDelay,Mathf.InverseLerp(5,0,Vector3.Distance(transform.position,item.transform.position)));
			Destroy(item,delay);
		}	
		u = Mathf.MoveTowards(u,uTarget,vel*Time.deltaTime);
		transform.position = bezier.calcBezier(u);
		if(u == uTarget){
			if(bezier.dir){
				bezier = bezier.dir as Bezier;
				u=uIni;
			}else{
				enabled = false;
			}
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position,radius);
	}
}
