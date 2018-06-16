using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class EnumFlagsAttribute : PropertyAttribute{
	public EnumFlagsAttribute() { }
}

[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer{
	public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
	{
		_property.intValue = EditorGUI.MaskField( _position, _label, _property.intValue, _property.enumNames );
	}
}

[System.Flags]
public enum FlockingMethods{
	Cohesion = (1 << 0),
	Separation = (1 << 1),
	Alignment = (1 << 2),
	VectorField = (1 << 3),
	Wind = (1 << 4),
	Leader = (1<<5),
	BoundBox = (1<<6),
	Predator = (1<<7),
}

public class Flocking : MonoBehaviour {

	
	[SerializeField] [EnumFlagsAttribute]public FlockingMethods rules;	
	public delegate Vector3 FlokingRule(FlockingAgent b);
	private List<FlokingRule> methods  = new List<FlokingRule>();
	public float separationDistance=1;
	public float predatorDistance=1;
	public float vel=3;
	public int velLimit=2;
	public float separation=1;
	public float cohesion=1;
	public float vectorField=1;
	public float alignment=1;
	public float boundBoxWeight=10;
	public float leader=1;
	public float predator=1;
		
	public VectorField vecField;
	public Wind wind;
	public Transform tLeader;
	public Transform tPredator;
	public BoxCollider boundBox; 
	public FlockingAgent[] flokingAgents;

	void Start () {
		if(flokingAgents == null || flokingAgents.Length<=0){
			flokingAgents = GameObject.FindObjectsOfType<FlockingAgent>();
		}
		RegisterMethods();
	}
	
	void RegisterMethods(){
		if((rules & FlockingMethods.Cohesion) == FlockingMethods.Cohesion){
			methods.Add(CohesionRule);
		}
		if((rules & FlockingMethods.Separation) == FlockingMethods.Separation){
			methods.Add(SeparationRule);
		}
		if((rules & FlockingMethods.Alignment) == FlockingMethods.Alignment){
			methods.Add(AlignmentRule);
		}
		if((rules & FlockingMethods.VectorField) == FlockingMethods.VectorField){
			methods.Add(VectorFieldRule);
		}
		if((rules & FlockingMethods.Wind) == FlockingMethods.Wind){
			methods.Add(WindDirection);
		}
		if((rules & FlockingMethods.Leader) == FlockingMethods.Leader){
			methods.Add(LeaderRule);
		}
		if((rules & FlockingMethods.BoundBox) == FlockingMethods.BoundBox){
			methods.Add(BoundBoxRule);
		}
		if((rules & FlockingMethods.Predator) == FlockingMethods.Predator){
			methods.Add(PredatorRule);
		}
	}
	
	void Update () {
		foreach(FlockingAgent b in flokingAgents){
			Vector3 result = Vector3.zero;
			foreach (var item in methods){
				result+= item(b);
			}
			b.velocity += result;
			LimitVelocity(b);
			b.transform.position+=b.velocity*vel*Time.deltaTime;
		}
	}
	
	Vector3 CohesionRule(FlockingAgent fa){
		
		Vector3 v = Vector3.zero;
		foreach(FlockingAgent item in flokingAgents){
			if(item != fa){
				v += item.transform.position;
			}
		}
		v = v/(flokingAgents.Length-1);	 
		return (v - fa.transform.position).normalized*cohesion;
	}
	
	Vector3 SeparationRule(FlockingAgent fa){
			
		Vector3 v = Vector3.zero;
		foreach(FlockingAgent item in flokingAgents){
			if(item != fa){
				if((item.transform.position - fa.transform.position).sqrMagnitude < separationDistance*separationDistance) 
					v -= (item.transform.position - fa.transform.position);
			}
		}
		v.Normalize();
		return v*separation;
	}
	
	Vector3 AlignmentRule(FlockingAgent fa){
	
		Vector3 v = Vector3.zero;	
		foreach(FlockingAgent item in flokingAgents){
			if(item != fa){
				v += item.velocity;
			}
		}			
		v /= (flokingAgents.Length-1);
		return ((v - fa.velocity)/8)*alignment;
	}
	
	Vector3 VectorFieldRule(FlockingAgent fa){	
		Vector3 v = Vector3.zero;	
		v = vecField.GetDirection(fa.transform.position);			
		return v*vectorField;
	}
	
	Vector3 WindDirection(FlockingAgent fa){
		return wind.direction;
	}
	
	Vector3 LeaderRule(FlockingAgent fa){			
		return (tLeader.position - fa.transform.position).normalized*leader;
	}
	
	Vector3 PredatorRule(FlockingAgent fa){			
		if((fa.transform.position - tPredator.position).sqrMagnitude < predatorDistance*predatorDistance)
			return -predator * (tPredator.position - fa.transform.position).normalized;
		return Vector3.zero;
	}
	
	void  LimitVelocity(FlockingAgent fa){
		if(fa.velocity.magnitude > velLimit){
			fa.velocity = (fa.velocity/fa.velocity.magnitude) * velLimit;
		}	
	}
	
	Vector3 BoundBoxRule(FlockingAgent fa){
		Vector3 v = Vector3.zero;
		float Xmin=boundBox.bounds.min.x, 
			  Xmax= boundBox.bounds.max.x, 
			  Ymin= boundBox.bounds.min.y, 
			  Ymax= boundBox.bounds.max.y, 
			  Zmin=boundBox.bounds.min.z, 
			  Zmax=boundBox.bounds.max.z;
		if(fa.transform.position.x < Xmin){
			v.x = boundBoxWeight;
		}else if(fa.transform.position.x > Xmax){
			v.x = -boundBoxWeight;
		}
		if(fa.transform.position.y < Ymin){
			v.y = boundBoxWeight;
		}else if(fa.transform.position.y > Ymax){
			v.y = -boundBoxWeight;
		}
		if(fa.transform.position.z < Zmin){
			v.z = boundBoxWeight;
		}else if(fa.transform.position.z > Zmax){
			v.z = -boundBoxWeight;
		}
		return v;
	}
}
