using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(Bezier))]
class SphereCap : Editor {
	
	float sphereSize  = 1f;
	
	void OnSceneGUI () {
	
		Bezier myTarget = (Bezier)target;
		sphereSize = myTarget.transform.localScale.magnitude/3;
		Handles.color = new Color(1,1,1,0.5f);
		if(myTarget.p1)
		myTarget.p1.position = Handles.FreeMoveHandle(
		    myTarget.p1.position,
				Quaternion.identity,sphereSize,myTarget.transform.localScale,Handles.SphereHandleCap);
		if(myTarget.p2)
		myTarget.p2.position = Handles.FreeMoveHandle(
			myTarget.p2.position,
				Quaternion.identity,sphereSize,myTarget.transform.localScale,Handles.SphereHandleCap);
		if(myTarget.p3)
		myTarget.p3.position = Handles.FreeMoveHandle(
			myTarget.p3.position,
				Quaternion.identity,sphereSize,myTarget.transform.localScale,Handles.SphereHandleCap);
		if(myTarget.p4)
		myTarget.p4.position = Handles.FreeMoveHandle(
			myTarget.p4.position,
				Quaternion.identity,sphereSize,myTarget.transform.localScale,Handles.SphereHandleCap);

	}
}