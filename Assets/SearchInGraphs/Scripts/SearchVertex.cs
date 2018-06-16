using UnityEngine;
using System.Collections;

public enum AreaState{
	normal,
	fire,
	water
}

public class SearchVertex: MonoBehaviour {

	public AreaState areaState;
	public SearchVertex down;
	public SearchVertex left;
	public SearchVertex up;
	public SearchVertex right;
}
