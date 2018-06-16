using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Profit{
	public AuctionObject auctionObj;
	public float benefit;
	[HideInInspector]
	public float gain;
}

public class Negotiator : MonoBehaviour {

	public Profit[] profits;
	public AuctionObject purchased;

	#if UNITY_EDITOR
	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		foreach (var item in profits) {
			if(item == null || !item.auctionObj) return;
			Gizmos.DrawLine(transform.position,item.auctionObj.transform.position);
			Handles.Label(transform.position + (item.auctionObj.transform.position-transform.position)*0.15f, item.benefit.ToString());
		}
		Gizmos.color = Color.red;
		if (purchased) {
			Gizmos.DrawLine(transform.position,purchased.transform.position);
		}
	}
	#endif
}
