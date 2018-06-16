using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AuctionObject : MonoBehaviour {

	public float price;
	
	#if UNITY_EDITOR
	void OnDrawGizmos(){
		Handles.Label(transform.position + transform.right, price.ToString());
	}
	#endif
}
