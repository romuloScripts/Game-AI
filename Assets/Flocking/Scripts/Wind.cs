using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wind : MonoBehaviour {

	public Vector3 direction;
	public float amount;
	public Text text;
	
	void Start () {
		InvokeRepeating("RandomWind",0,5);
		InvokeRepeating("WithoutWind",11,11);
	}
	
	public void RandomWind(){
		Vector3 dir = new Vector3(Random.Range(-amount,amount),0,Random.Range(-amount,amount));
		direction = dir;
		text.text = "Wind: "+ direction;
		Debug.Log(text.text);
	}
	
	public void WithoutWind(){
		direction = Vector3.zero;
		text.text = "Wind: "+ direction;
		Debug.Log(text.text);
	}
	
	
}
