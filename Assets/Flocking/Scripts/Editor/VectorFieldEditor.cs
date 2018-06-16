using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(VectorField))]
public class VectorFieldEditor : Editor {

	
	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		
		VectorField myTarget= (VectorField)target;
		if(GUILayout.Button("Generate Matrix")){
			myTarget.GenerateMatrix();
		}
		if(GUILayout.Button("Save Rotations")){
			myTarget.SaveRotations();
		}
		if(GUILayout.Button("Load Rotations")){
			myTarget.LoadRotations();
		}
		if(GUILayout.Button("Add Colliders")){
			myTarget.AddColliders();
		}
		if(GUILayout.Button("Remove Colliders")){
			myTarget.RemoveColliders();
		}	
	}
	
	public void OnSceneGUI(){
		VectorField myTarget= (VectorField)target;
		Transform[] transforms  = myTarget.transforms;
		if(transforms == null) return;
		for (int i = 0; i < transforms.Length; i++) {
			if(myTarget.pathPassed != null && myTarget.pathPassed.Length >0 && myTarget.pathPassed[i]){
				Handles.color = Color.red;
			}else{
				Handles.color = Color.white;
			}
			Handles.ArrowHandleCap(i,transforms[i].position,transforms[i].rotation,3,EventType.Repaint);
			Vector3 rot =  transforms[i].eulerAngles;
			transforms[i].rotation=  Handles.FreeRotateHandle(transforms[i].rotation,transforms[i].position,1);
			Vector3 rot2 =  transforms[i].eulerAngles;
			rot.y = rot2.y;
			transforms[i].rotation = Quaternion.Euler(rot);
			
		}
	}
}
