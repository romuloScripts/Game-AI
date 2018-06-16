using UnityEngine;
using System.Collections;
using System.Linq;

public class VectorField : MonoBehaviour {

	public int matrixLength=10;
	public int length=100;
	public Transform [] transforms;
	public Quaternion [] rotations;

	[HideInInspector]
	public bool[] pathPassed;
	void Start () {
		pathPassed = new bool[transforms.Length];
	}
	
	public void GenerateMatrix(){
		Transform[] ts = transform.Cast<Transform>().ToArray();
		foreach (var item in ts) {
			DestroyImmediate(item.gameObject);
		}
		transforms = new Transform[matrixLength*matrixLength];
		for (int i = 0; i < matrixLength; i++) {
			for (int j = 0; j < matrixLength; j++) {
				
				transforms[j+(matrixLength*i)] = new GameObject("Vetor "+ i + "/" + j,typeof(SphereCollider)).transform;
				if(transforms[j+(matrixLength*i)]){
					Debug.Log(transforms[j+(matrixLength*i)].name);
				}
				float cellLength = (float)length/(float)matrixLength;
				Vector3 pos = new Vector3(transform.position.x + (i*cellLength+(cellLength/2)),
										  transform.position.y,
				                          transform.position.z + (j*cellLength+(cellLength/2)));
				transforms[j+(matrixLength*i)].position = pos;
				transforms[j+(matrixLength*i)].parent = transform; 
			}
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawLine(transform.position,transform.position+Vector3.right*length);
		Gizmos.DrawLine(transform.position,transform.position+Vector3.forward*length);
		Gizmos.DrawLine(transform.position+Vector3.forward*length,(transform.position+Vector3.forward*length)+Vector3.right*length);
		Gizmos.DrawLine(transform.position+Vector3.right*length,(transform.position+Vector3.right*length)+Vector3.forward*length);
	}
	
	public void SaveRotations(){
		rotations = new Quaternion[transforms.Length];
		for (int i = 0; i < transforms.Length; i++) {
			rotations[i] = transforms[i].rotation;
		}
	}
	
	public void LoadRotations(){
		for (int i = 0; i < transforms.Length; i++) {
			transforms[i].rotation = rotations[i];
		}
	}
	
	public void AddColliders(){
		for (int i = 0; i < transforms.Length; i++) {
			transforms[i].gameObject.AddComponent<SphereCollider>();
		}
	}
	
	public void RemoveColliders(){
		for (int i = 0; i < transforms.Length; i++) {
			if(transforms[i].GetComponent<Collider>())
				DestroyImmediate(transforms[i].GetComponent<Collider>());
		}
	}
	
	public Vector3 GetDirection(Vector3 pos){
		float cellLength = (float)length/(float)matrixLength;
		bool testX=false,testY=false;
		int x=0;
		int y=0;
		for (int i = 0; i < matrixLength; i++) {
			if((transform.position.x + (i*cellLength+(cellLength/2))) > pos.x){
				x=i;
				testX = true;
				break;
			}
		}
		for (int i = 0; i < matrixLength ; i++) {
			if((transform.position.z + (i*cellLength+(cellLength/2))) > pos.z){
				y=i;
				testY = true;
				break;
			}
		}
		if(!testX)
			x = matrixLength-1;
		if(!testY)
			y = matrixLength-1;
		pathPassed[y+(matrixLength*x)] =true;
		return transforms[y+(matrixLength*x)].forward.normalized;
	}
	
}
